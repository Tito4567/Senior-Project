namespace LacesAPI.Models.Request
{
    public class UserFollowRequest : LacesRequest
    {
        public int FollowedUserId { get; set; }
    }
}
