using System.Collections.Generic;
namespace LacesViewModel.Request
{
    public class AddProductRequest : LacesRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SellerId { get; set; }
        public double AskingPrice { get; set; }
        public int ProductStatudId { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public int ProductTypeId { get; set; }
        public int ConditionId { get; set; }
        public List<ImageInfo> Images { get; set; }
    }
}
