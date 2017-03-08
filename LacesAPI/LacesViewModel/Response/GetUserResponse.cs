using System;
using System.Collections.Generic;

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
        public int ProductCount { get; set; }
        public int FollowedUsers { get; set; }
        public int FollowingUsers { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProfilePicture ProfilePicture { get; set; }
		public List<int> Products { get; set; }
    }

    public class ProfilePicture
    {
        public string fileName { get; set; }
        public string fileFormat { get; set; }
        public byte[] fileData { get; set; }
        public DateTime DateLastChanged { get; set; }
    }
}
