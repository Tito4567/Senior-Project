using System.Collections.Generic;

namespace LacesAPI.Models.Response
{
    public class GetInterestFeedResponse : LacesResponse
    {
        public List<int> Products { get; set; }
    }
}
