import java.util.Date;

public class ProductInfo
{
	private ImageInfo[] ProductImages;
	private int CommentCount;
	private int LikeCount;
	private String Name;
	private String Description;
	private String Brand;
	private String Size;
	private int ConditionId;
	private Date CreatedDate;
	private double AskingPrice;
	private int[] Comments;
	private TagInfo[] Tags;
	
	public ImageInfo[] getProductImages()
	{
		return ProductImages;
	}
	
	public int getCommentCount()
	{
		return CommentCount;
	}
	
	public int getLikeCount()
	{
		return LikeCount;
	}
	
	public String getName()
	{
		return Name;
	}
	
	public String getDescription()
	{
		return Description;
	}
	
	public String getBrand()
	{
		return Brand;
	}
	
	public String getSize()
	{
		return Size;
	}
	
	public int getConditionId()
	{
		return ConditionId;
	}
	
	public Date getCreatedDate()
	{
		return CreatedDate;
	}
	
	public double getAskingPrice()
	{
		return AskingPrice;
	}
	
	public int[] getComments()
	{
		return Comments;
	}
	
	public TagInfo[] getTags()
	{
		return Tags;
	}
	
	public void setProductImages(ImageInfo[] ProductImages)
	{
		this.ProductImages = ProductImages;
	}
	
	public void setCommentCount(int CommentCount)
	{
		this.CommentCount = CommentCount;
	}
	
	public void setLikeCount(int LikeCount)
	{
		this.LikeCount = LikeCount;
	}
	
	public void setName(String Name)
	{
		this.Name = Name;
	}
	
	public void setDescription(String Description)
	{
		this.Description = Description;
	}
	
	public void setBrand(String Brand)
	{
		this.Brand = Brand;
	}
	
	public void setSize(String Size)
	{
		this.Size = Size;
	}
	
	public void setConditionId(int ConditionId)
	{
		this.Conditionid = Conditionid;
	}
	
	public void setCreatedDate(Date CreatedDate)
	{
		this.CreatedDate = CreatedDate;
	}
	
	public void setAskingPrice(double AskingPrice)
	{
		this.AskingPrice = AskingPrice;
	}
	
	public void setComments(int[] Comments)
	{
		this.Comments = Comments;
	}
	
	public void setTags(TagInfo[] Tags)
	{
		this.Tags = Tags;
	}
}