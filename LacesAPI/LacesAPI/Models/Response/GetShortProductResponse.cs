namespace LacesAPI.Models.Response
{
    public class GetShortProductResponse : LacesResponse
    {
        public string UserName { get; set; }
        public ImageInfo UserProfilePic { get; set; }
        public ImageInfo ProductImage { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
    }
}
