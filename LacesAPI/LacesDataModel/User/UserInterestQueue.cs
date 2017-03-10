using LacesRepo;
using LacesRepo.Attributes;
using System;
using System.Collections.Generic;

namespace LacesDataModel.User
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_USER_INTEREST_QUEUE)]
    [PrimaryKeyName("UserInterestQueueId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class UserInterestQueue : DataObject
    {
        public int UserInterestQueueId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public bool Interested { get; set; }

        public UserInterestQueue() { }

        public UserInterestQueue(int id) : base(id) { }

        public override void Load(int id)
        {
            UserInterestQueue temp = GetByValue<UserInterestQueue>("UserInterestQueueId", Convert.ToString(id), Constants.TABLE_USER_INTEREST_QUEUE, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                UserInterestQueueId = temp.UserInterestQueueId;
                UserId = temp.UserId;
                ProductId = temp.ProductId;
                Interested = temp.Interested;
            }
            else
            {
                throw new Exception("Could not find interest queue record with Id " + id);
            }
        }

        public void LoadByUserAndProductIds(int userId, int productId)
        {
            SearchEntity search = new SearchEntity();

            search.ConnectionString = Constants.CONNECTION_STRING;
            search.Conditions = new List<Condition>();

            Condition followerCond = new Condition();
            followerCond.Column = "UserId";
            followerCond.Operator = Condition.Operators.EqualTo;
            followerCond.Value = Convert.ToString(userId);

            Condition followedCond = new Condition();
            followedCond.Column = "ProductId";
            followedCond.Operator = Condition.Operators.EqualTo;
            followedCond.Value = Convert.ToString(productId);

            search.Conditions.Add(followerCond);
            search.Conditions.Add(followedCond);

            search.PageSizeLimit = 1;
            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_USER_FOLLOWS;

            List<UserInterestQueue> response = new GenericRepository<UserInterestQueue>().Read(search);

            if (response.Count > 0)
            {
                UserInterestQueueId = response[0].UserInterestQueueId;
                UserId = response[0].UserId;
                ProductId = response[0].ProductId;
                Interested = response[0].Interested;
            }
            else
            {
                UserInterestQueueId = 0;
                UserId = 0;
                ProductId = 0;
                Interested = false;
            }
        }
    }
}
