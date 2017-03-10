namespace LacesViewModel.Request
{
    public class ProcessTransactionRequest : LacesRequest
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public string ReferenceNumber { get; set; }
        public double Amount { get; set; }
    }
}
