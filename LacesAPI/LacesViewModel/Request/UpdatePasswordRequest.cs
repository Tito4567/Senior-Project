namespace LacesViewModel.Request
{
    public class UpdatePasswordRequest : LacesRequest
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
