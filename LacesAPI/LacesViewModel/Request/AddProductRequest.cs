using System.Collections.Generic;
namespace LacesViewModel.Request
{
    public class AddProductRequest : LacesRequest
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal AskingPrice { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public int ProductTypeId { get; set; }
        public int ConditionId { get; set; }
        public List<ImageInfo> Images { get; set; }
        public List<string> Tags { get; set; }
    }
}
