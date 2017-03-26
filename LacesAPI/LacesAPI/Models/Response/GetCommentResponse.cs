using System;

namespace LacesAPI.Models.Response
{
    public class GetCommentResponse : LacesResponse
    {
        public string UserName { get; set; }
        public ImageInfo UserImage { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
