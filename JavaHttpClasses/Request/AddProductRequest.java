public class AddProductRequest extends LacesRequest
{
	private String ProductName;
	private String Description;
	private double AskingPrice;
	private String Brand;
	private String Size;
	private int ProductTypeId;
	private int ConditionId;
	private ImageInfoRequest[] Images;
	private String[] Tags;
	
	public String getProductName()
	{
		return ProductName;
	}
	
	public String getDescription()
	{
		return Description;
	}
	
	public double getAskingPrice()
	{
		return AskingPrice;
	}
	
	public String getBrand()
	{
		return Brand;
	}
	
	public String getSize()
	{
		return Size;
	}
	
	public int getProductTypeId()
	{
		return ProductTypeId;
	}
	
	public int getConditionId()
	{
		return ConditionId;
	}
	
	public ImageInfoRequest[] getImages()
	{
		return Images;
	}
	
	public String[] getTags()
	{
		return Tags;
	}
	
	public void setProductName(String ProductName)
	{
		this.ProductName = ProductName;
	}
	
	public void setDescription(String Description)
	{
		this.Description = Description;
	}
	
	public void setAskingPrice(double AskingPrice)
	{
		this.AskingPrice = AskingPrice;
	}
	
	public void setBrand(String Brand)
	{
		this.Brand = Brand;
	}
	
	public void setSize(String Size)
	{
		this.Size = Size;
	}
	
	public void setProductTypeId(int ProductTypeId)
	{
		this.ProductTypeId = ProductTypeId;
	}
	
	public void setConditionId(int ConditionId)
	{
		this.ConditionId = ConditionId;
	}
	
	public void setImages(ImageInfoRequest[] Images)
	{
		this.Images = Images;
	}
	
	public void setTags(String[] Tags)
	{
		this.Tags = Tags;
	}
}