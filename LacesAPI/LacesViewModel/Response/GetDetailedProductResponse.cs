using System;
using System.Collections.Generic;
namespace LacesViewModel.Response
{
    public enum UserInterestStatusOption
    {
        Unknown = 0
        , Interested = 1
        , Uninterested = 2
    }

    public class GetDetailedProductResponse : LacesResponse
    {
        public string UserName { get; set; }
        public ImageInfo UserProfilePic { get; set; }
        public Product Product { get; set; }
        public int UserInterestStatus { get; set; }
    }

    public class Product
    {
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
        public List<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public int TagId { get; set; }
        public string Description { get; set; }
    }
}
