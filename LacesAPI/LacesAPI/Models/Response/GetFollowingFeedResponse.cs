using System;
using System.Collections.Generic;

namespace LacesAPI.Models.Response
{
    public class GetFollowingFeedResponse : LacesResponse
    {
        public List<FeedItem> Products { get; set; }
    }

    public class FeedItem
    {
        public int ProductId { get; set; }
        public string FeedResultTypeMessage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
