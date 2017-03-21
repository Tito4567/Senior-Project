public class AddUserRequest extends LacesRequest
{
	private String UserName;
	private String Password;
	private String DisplayName;
	private String Description;
	private String Email;
	
	public String getUserName()
	{
		return UserName;
	}
	
	public String getPassword()
	{
		return Password;
	}
	
	public String getDisplayName()
	{
		return DisplayName;
	}
	
	public String getDescription()
	{
		return Description;
	}
	
	public String getEmail()
	{
		return Email;
	}
	
	public void setUserName(String UserName)
	{
		this.UserName = UserName;
	}
	
	public void setPassword(String Password)
	{
		this.Password = Password;
	}
	
	public void setDisplayName(String DisplayName)
	{
		this.DisplayName = DisplayName;
	}
	
	public void setDescription(String Description)
	{
		this.Description = Description;
	}
	
	public void setEmail(String Email)
	{
		this.Email = Email;
	}
}