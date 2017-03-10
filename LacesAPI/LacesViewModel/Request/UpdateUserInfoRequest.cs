namespace LacesViewModel.Request
{
    public class UpdateUserInfoRequest : LacesRequest
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
