import java.util.Date;

public class FeedItem
{
	private int ProductId;
	private String FeedResultTypeMessage;
	private Date CreatedDate;
	
	public int getProductId()
	{
		return ProductId;
	}
	
	public String getFeedResultTypeMessage()
	{
		return FeedResultTypeMessage;
	}
	
	public Date getCreatedDate()
	{
		return CreatedDate;
	}
	
	public void setProductId(int ProductId)
	{
		this.ProductId = ProductId;
	}
	
	public void setFeedResultTypeMessage(String FeedResultTypeMessage)
	{
		this.FeedResultTypeMessage = FeedResultTypeMessage;
	}
	
	public void setCreatedDate(Date CreatedDate)
	{
		this.CreatedDate = CreatedDate;
	}
}