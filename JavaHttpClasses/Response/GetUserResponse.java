public class GetUserResponse extends LacesResponse
{
	private UserInfo User;
	private boolean IsFollowing;
    private boolean IsBeingFollowed;
	
	public UserInfo getUser()
	{
		return User;
	}
	
	public boolean getIsFollowing()
	{
		return IsFollowing;
	}
	
	public boolean getIsBeingFollowed()
	{
		return IsBeingFollowed;
	}
	
	public void setUser(UserInfo User)
	{
		this.User = User;
	}
	
	public void setIsFollowing(boolean IsFollowing)
	{
		this.IsFollowing = IsFollowing;
	}
	
	public void setIsBeingFollowed(boolean IsBeingFollowed)
	{
		this.IsBeingFollowed = IsBeingFollowed;
	}
}