namespace LacesViewModel.Request
{
    public class AddCommentRequest : LacesRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
    }
}
