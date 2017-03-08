using LacesRepo.Attributes;
using System;

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
            }
            else
            {
                throw new Exception("Could not find user follow record with Id " + id);
            }
        }
    }
}
