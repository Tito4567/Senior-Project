namespace LacesViewModel.Request
{
    public class AddTagRequest : LacesRequest
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
    }
}
