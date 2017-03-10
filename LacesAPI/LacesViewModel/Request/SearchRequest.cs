using System.Collections.Generic;
namespace LacesViewModel.Request
{
    public class SearchRequest : LacesRequest
    {
        public int SearchType { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> Tags { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string Size { get; set; }
        public string Brand { get; set; }
    }
}
