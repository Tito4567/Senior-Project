namespace LacesViewModel.Request
{
    public class FollowUserRequest : LacesRequest
    {
        public int FollowedUserId { get; set; }
        public int FollowingUserId { get; set; }
    }
}
