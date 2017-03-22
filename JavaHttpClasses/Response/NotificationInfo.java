import java.util.Date;

public class NotificationInfo
{
	private String UserName;
	private int ProductId;
	private Date CreatedDate;
	private int NotificationType;
	
	public String getUserName()
	{
		return UserName;
	}
	
	public int getProductId()
	{
		return ProductId;
	}
	
	public Date getCreatedDate()
	{
		return CreatedDate;
	}
	
	public int getNotificationType()
	{
		return NotificationType;
	}
	
	public void setUserName(String UserName)
	{
		this.UserName = UserName;
	}
	
	public void setProductId(int ProductId)
	{
		this.ProductId = ProductId;
	}
	
	public void setCreatedDate(Date CreatedDate)
	{
		this.CreatedDate = CreatedDate;
	}
	
	public void setNotificationType(int NotificationType)
	{
		this.NotificationType = NotificationType;
	}
}