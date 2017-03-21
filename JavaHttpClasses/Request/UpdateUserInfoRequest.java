public class UpdateUserInfoRequest extends LacesRequest
{
	private String DisplayName;
	private String Description;
	
	public String getDisplayName()
	{
		return DisplayName;
	}
	
	public String getDescription()
	{
		return Description;
	}
	
	public void setDisplayName(String DisplayName)
	{
		this.DisplayName = DisplayName;
	}
	
	public void setDescription(String Description)
	{
		this.Description = Description;
	}
}