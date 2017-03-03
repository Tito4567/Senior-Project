namespace LacesViewModel.Request
{
    public class AddUserRequest : LacesRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }
}
