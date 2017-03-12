using System.Collections.Generic;
namespace LacesViewModel.Request
{
    public class SearchRequest : LacesRequest
    {
        public int SearchType { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> Tags { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Size { get; set; }
        public string Brand { get; set; }
    }
}
