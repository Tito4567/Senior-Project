public class AddUserResponse extends LacesResponse
{
	private boolean UserNameTaken;
	private boolean EmailTaken;
	
	public boolean getUserNameTaken()
	{
		return UserNameTaken;
	}
	
	public boolean getEmailTaken()
	{
		return EmailTaken;
	}
	
	public void setUserNameTaken(boolean UserNameTaken)
	{
		this.UserNameTaken = UserNameTaken;
	}
	
	public void setEmailTaken(boolean EmailTaken)
	{
		this.EmailTaken = EmailTaken;
	}
}