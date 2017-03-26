public class GetDetailedProductResponse extends LacesResponse
{
	private String UserName;
	private ImageInfo UserProfilePic;
	private ProductInfo Product;
	private int UserInterestStatus;
	
	public String getUserName()
	{
		return UserName;
	}
	
	public ImageInfo getUserProfilePic()
	{
		return UserProfilePic;
	}
	
	public ProductInfo getProduct()
	{
		return Product;
	}
	
	public int getUserInterestStatus()
	{
		return UserInterestStatus;
	}
	
	public void setUserName(String UserName)
	{
		this.UserName = UserName;
	}
	
	public void setUserProfilePic(ImageInfo UserProfilePic)
	{
		this.UserProfilePic = UserProfilePic;
	}
	
	public void setProduct(ProductInfo Product)
	{
		this.Product = Product;
	}
	
	public void setUserInterestStatus(int UserInterestStatus)
	{
		this.UserInterestStatus = UserInterestStatus;
	}
}