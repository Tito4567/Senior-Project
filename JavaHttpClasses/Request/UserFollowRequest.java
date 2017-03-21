public class UserFollowRequest extends LacesRequest
{
	private int FollowedUserId;
	
	public int getFollowedUserId()
	{
		return FollowedUserId;
	}
	
	public void setFollowedUserId(int FollowedUserId)
	{
		this.FollowedUserId = FollowedUserId;
	}
}