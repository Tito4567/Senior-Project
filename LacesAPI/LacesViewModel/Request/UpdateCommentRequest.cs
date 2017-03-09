namespace LacesViewModel.Request
{
    public class UpdateCommentRequest : LacesRequest
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
    }
}
