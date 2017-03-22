public class GetShortProductResponse extends LacesResponse
{
	private String UserName;
	private ImageInfo UserProfilePic;
	private ImageInfo ProductImage;
	private int CommentCount;
	private int LikeCount;
	
	public String getUserName()
	{
		return UserName;
	}
	
	public ImageInfo getUserProfilePic()
	{
		return UserProfilePic;
	}
	
	public ImageInfo getProductImage()
	{
		return ProductImage;
	}
	
	public int getCommentCount()
	{
		return CommentCount;
	}
	
	public int getLikeCount()
	{
		return LikeCount;
	}
	
	public void setUserName(String UserName)
	{
		this.UserName = UserName;
	}
	
	public void setUserProfilePic(ImageInfo UserProfilePic)
	{
		this.UserProfilePic = UserProfilePic;
	}
	
	public void setProductImage(ImageInfo ProductImage)
	{
		this.ProductImage = ProductImage;
	}
	
	public void setCommentCOunt(int CommentCount)
	{
		this.CommentCount = CommentCount;
	}
	
	public void setLikeCount(int LikeCount)
	{
		this.LikeCount = LikeCount;
	}
}