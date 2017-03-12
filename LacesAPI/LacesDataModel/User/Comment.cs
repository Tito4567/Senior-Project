using LacesRepo;
using LacesRepo.Attributes;
using System;
using System.Collections.Generic;

namespace LacesDataModel.User
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_COMMENTS)]
    [PrimaryKeyName("CommentId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class Comment : DataObject
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Comment() { }

        public Comment(int id) : base(id) { }

        public override void Load(int id)
        {
            Comment temp = GetByValue<Comment>("Commentid", Convert.ToString(id), Constants.TABLE_COMMENTS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                CommentId = temp.CommentId;
                UserId = temp.UserId;
                ProductId = temp.ProductId;
                Text = temp.Text;
                CreatedDate = temp.CreatedDate;
                UpdatedDate = temp.UpdatedDate;
            }
            else
            {
                throw new Exception("Could not find comment with Id " + id);
            }
        }

        // This might be best moved into a different class later on.
        public static List<Comment> GetCommentsForProduct(int productId)
        {
            List<Comment> result = new List<Comment>();

            SearchEntity search = new SearchEntity();

            search.ColumnsToReturn = new List<string>();
            search.ColumnsToReturn.Add("CommentId");

            search.ConnectionString = Constants.CONNECTION_STRING;

            LacesRepo.Condition userIdCond = new LacesRepo.Condition();
            userIdCond.Column = "ProductId";
            userIdCond.Operator = LacesRepo.Condition.Operators.EqualTo;
            userIdCond.Value = Convert.ToString(productId);

            search.Conditions.Add(userIdCond);

            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_IMAGES;

            result = new GenericRepository<Comment>().Read(search);

            return result;
        }
    }
}
