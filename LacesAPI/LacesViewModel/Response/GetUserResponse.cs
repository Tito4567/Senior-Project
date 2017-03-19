using System;
using System.Collections.Generic;

namespace LacesViewModel.Response
{
    public class GetUserResponse : LacesResponse
    {
        public User User { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsBeingFollowed { get; set; }
    }

    public class User
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int ProductCount { get; set; }
        public int FollowedUsers { get; set; }
        public int FollowingUsers { get; set; }
        public DateTime CreatedDate { get; set; }
        public ImageInfo ProfilePicture { get; set; }
		public List<int> Products { get; set; }
    }

    public class ImageInfo
    {
        public string FileName { get; set; }
        public string FileFormat { get; set; }
        public byte[] FileData { get; set; }
        public DateTime DateLastChanged { get; set; }
    }
}
