import java.util.Date;

public class GetCommentResponse extends LacesResponse
{
	private String UserName;
	private ImageInfo UserImage;
	private String Text;
	private Date CreatedDate;
	private Date UpdatedDate;
	
	public String getUserName()
	{
		return UserName;
	}
	
	public ImageInfo getUserImage()
	{
		return UserImage;
	}
	
	public String getText()
	{
		return Text;
	}
	
	public Date getCreatedDate()
	{
		return CreatedDate;
	}
	
	public Date getUpdatedDate()
	{
		return UpdatedDate;
	}
	
	public void setUserName(String UserName)
	{
		this.UserName = UserName;
	}
	
	public void setUserImage(ImageInfo UserImage)
	{
		this.UserImage = UserImage;
	}
	
	public void setText(String text)
	{
		this.Text = Text;
	}
	
	public void setCreatedDate(Date CreatedDate)
	{
		this.CreatedDate = CreatedDate;
	}
	
	public void setUpdatedDate(Date UpdatedDate)
	{
		this.UpdatedDate = UpdatedDate;
	}
}