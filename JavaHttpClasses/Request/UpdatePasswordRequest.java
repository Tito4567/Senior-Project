public class UpdatePasswordRequest extends LacesRequest
{
	private String OldPassword;
	private String NewPassword;
	
	public String getOldPassword()
	{
		return OldPassword;
	}
	
	public String getNewPassword()
	{
		return NewPassword;
	}
	
	public void setOldPassword(String OldPassword)
	{
		this.OldPassword = OldPassword;
	}
	
	public void setNewPassword(String NewPassword)
	{
		this.NewPassword = NewPassword;
	}
}