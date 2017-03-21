public class AddImageRequest extends LacesRequest
{
	private int AssociatedEntityId;
	private ImageInfoRequest ImageInfo;
	
	public int getAssociatedEntityId()
	{
		return AssociatedEntityId;
	}
	
	public ImageInfoRequest getImageInfo()
	{
		return ImageInfo;
	}
	
	public void setAssociatedEntityId(int AssociatedEntityId)
	{
		this.AssociatedEntityId = AssociatedEntityId;
	}
	
	public void setImageInfo(ImageInfoRequest ImageInfo)
	{
		this.ImageInfo = ImageInfo;
	}
}