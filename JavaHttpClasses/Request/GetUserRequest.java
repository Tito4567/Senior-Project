public class GetUserRequest extends LacesRequest
{
	private int UserIdToGet;
	
	public int getUserIdToGet()
	{
		return UserIdToGet;
	}
	
	public void setUserIdToGet(int UserIdToGet)
	{
		this.UserIdToGet = UserIdToGet;
	}
}