namespace LacesViewModel.Request
{
    public class AddImageRequest : LacesRequest
    {
        public int AssociatedEntityId { get; set; }
        public ImageInfo ImageInfo { get; set; }
    }

    public class ImageInfo
    {
        public byte[] ImageData { get; set; }
        public string FileFormat { get; set; }
    }
}
