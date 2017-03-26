public class UpdateProductRequest extends LacesRequest
{
	private String ProductName;
	private String Description;
	private double AskingPrice;
	private int ProductStatusId;
	private String Brand;
	private String Size;
	private int ProductTypeId;
	private int ConditionId;
	
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
	
	public int getProductStatusId()
	{
		return ProductStatusId;
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
	
	public void setProductStatusId(int ProductStatusId)
	{
		this.ProductStatusId = ProductStatusId;
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
}