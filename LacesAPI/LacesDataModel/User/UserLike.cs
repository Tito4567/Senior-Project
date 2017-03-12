using LacesRepo;
using LacesRepo.Attributes;
using System;
using System.Collections.Generic;

namespace LacesDataModel.User
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_USER_LIKES)]
    [PrimaryKeyName("UserLikeId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class UserLike : DataObject
    {
        public int UserLikeId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserLike() { }

        public UserLike(int id) : base(id) { }

        public override void Load(int id)
        {
            UserLike temp = GetByValue<UserLike>("UserLikeId", Convert.ToString(id), Constants.TABLE_USER_LIKES, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                UserLikeId = temp.UserLikeId;
                UserId = temp.UserId;
                ProductId = temp.ProductId;
                CreatedDate = temp.CreatedDate;
            }
            else
            {
                throw new Exception("Could not find like with Id " + id);
            }
        }

        // This might be best moved into a different class later on.
        public static List<UserLike> GetLikesForProduct(int productId)
        {
            List<UserLike> result = new List<UserLike>();

            SearchEntity search = new SearchEntity();

            search.ColumnsToReturn = new List<string>();
            search.ColumnsToReturn.Add("UserLikeId");

            search.ConnectionString = Constants.CONNECTION_STRING;

            LacesRepo.Condition searchCond = new LacesRepo.Condition();
            searchCond.Column = "ProductId";
            searchCond.Operator = LacesRepo.Condition.Operators.EqualTo;
            searchCond.Value = Convert.ToString(productId);

            search.Conditions.Add(searchCond);

            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_USER_LIKES;

            result = new GenericRepository<UserLike>().Read(search);

            return result;
        }
    }
}
