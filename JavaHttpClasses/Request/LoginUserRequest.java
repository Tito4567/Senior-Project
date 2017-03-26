public class LoginUserRequest
{
	private String UserName;
	private String Password;
	
	
	public String getUserName()
	{
		return UserName;
	}
	
	public String getPassword()
	{
		return Password;
	}
	
	public void setUserName(String UserName)
	{
		this.UserName = UserName;
	}
	
	public void setPassword(String Password)
	{
		this.Password = Password;
	}
}