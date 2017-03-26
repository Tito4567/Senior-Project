namespace LacesAPI.Models.Request
{
    public class UpdateProductRequest : ProductRequest
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal AskingPrice { get; set; }
        public int ProductStatusId { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public int ProductTypeId { get; set; }
        public int ConditionId { get; set; }
    }
}
