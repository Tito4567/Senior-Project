namespace LacesViewModel.Request
{
    public class LikeProductRequest : LacesRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
