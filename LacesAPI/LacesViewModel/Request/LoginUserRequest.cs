namespace LacesViewModel.Request
{
    public class LoginUserRequest :LacesRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
