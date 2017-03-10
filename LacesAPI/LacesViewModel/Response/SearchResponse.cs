using System.Collections.Generic;
namespace LacesViewModel.Response
{
    public class SearchResponse : LacesResponse
    {
        public List<int> Users { get; set; }
        public List<int> Products { get; set; }
    }
}
