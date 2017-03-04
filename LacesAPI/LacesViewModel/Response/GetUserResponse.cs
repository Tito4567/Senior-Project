using System;

namespace LacesViewModel.Response
{
    public class GetUserResponse : LacesResponse
    {
        public User User { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
