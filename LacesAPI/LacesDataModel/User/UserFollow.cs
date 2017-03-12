using LacesRepo;
using LacesRepo.Attributes;
using System;
using System.Collections.Generic;

namespace LacesDataModel.User
{
    [ConnectionString(Constants.CONNECTION_STRING)]
    [TableName(Constants.TABLE_USER_FOLLOWS)]
    [PrimaryKeyName("UserFollowId")]
    [SchemaName(Constants.SCHEMA_DEFAULT)]
    public class UserFollow : DataObject
    {
        public int UserFollowId { get; set; }
        public int FollowingUserId { get; set; }
        public int FollowedUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserFollow() { }

        public UserFollow(int id) : base(id) { }

        public override void Load(int id)
        {
            UserFollow temp = GetByValue<UserFollow>("UserFollowId", Convert.ToString(id), Constants.TABLE_USER_FOLLOWS, Constants.SCHEMA_DEFAULT);

            if (temp != null)
            {
                UserFollowId = temp.UserFollowId;
                FollowingUserId = temp.FollowingUserId;
                FollowedUserId = temp.FollowedUserId;
                CreatedDate = temp.CreatedDate;
            }
            else
            {
                throw new Exception("Could not find user follow record with Id " + id);
            }
        }

        public void LoadByUserids(int followerId, int followedId)
        {
            SearchEntity search = new SearchEntity();

            search.ConnectionString = Constants.CONNECTION_STRING;

            Condition followerCond = new Condition();
            followerCond.Column = "FollowingUserId";
            followerCond.Operator = Condition.Operators.EqualTo;
            followerCond.Value = Convert.ToString(followerId);

            Condition followedCond = new Condition();
            followedCond.Column = "FollowedUserId";
            followedCond.Operator = Condition.Operators.EqualTo;
            followedCond.Value = Convert.ToString(followedId);

            search.Conditions.Add(followerCond);
            search.Conditions.Add(followedCond);

            search.PageSizeLimit = 1;
            search.SchemaName = Constants.SCHEMA_DEFAULT;
            search.TableName = Constants.TABLE_USER_FOLLOWS;

            List<UserFollow> response = new GenericRepository<UserFollow>().Read(search);

            if (response.Count > 0)
            {
                UserFollowId = response[0].UserFollowId;
                FollowingUserId = response[0].FollowingUserId;
                FollowedUserId = response[0].FollowedUserId;
            }
            else
            {
                UserFollowId = 0;
                FollowingUserId = 0;
                FollowedUserId = 0;
            }
        }
    }
}
