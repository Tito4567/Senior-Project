using System;
using System.Collections.Generic;
namespace LacesViewModel.Response
{
    public class GetProductResponse : LacesResponse
    {
        public string UserName { get; set; }
        public ImageInfo UserProfilePic { get; set; }
        public List<ImageInfo> ProductImages { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public int ConditionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public double AskingPrice { get; set; }
        public List<int> Comments { get; set; }
    }
}
