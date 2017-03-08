namespace LacesViewModel.Request
{
    public class UpdateUserInfoRequest : LacesRequest
    {
        public int UserId;
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
