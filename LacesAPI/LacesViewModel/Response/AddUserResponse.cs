namespace LacesViewModel.Response
{
    public class AddUserResponse : LacesResponse
    {
        public bool UserNameTaken { get; set; }
        public bool EmailTaken { get; set; }
    }
}
