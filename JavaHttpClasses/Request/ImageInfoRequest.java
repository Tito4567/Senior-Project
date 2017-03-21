public class ImageInfoRequest
{
	private byte[] ImageData;
	private String FileFormat;
	
	public byte[] getImageData()
	{
		return ImageData;
	}
	
	public String getFileFormat()
	{
		return FileFormat;
	}
	
	public void setImageData(byte[] ImageData)
	{
		this.ImageData = ImageData;
	}
	
	public void setFileFormat(String FileFormat)
	{
		this.FileFormat = FileFormat;
	}
}