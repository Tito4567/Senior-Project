namespace LacesViewModel.Request
{
    public class UpdateProductRequest : LacesRequest
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double AskingPrice { get; set; }
        public int ProductStatudId { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public int ProductTypeId { get; set; }
        public int ConditionId { get; set; }
    }
}
