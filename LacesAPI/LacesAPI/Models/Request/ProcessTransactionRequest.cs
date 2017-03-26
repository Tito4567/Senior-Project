namespace LacesAPI.Models.Request
{
    public class ProcessTransactionRequest : ProductRequest
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
