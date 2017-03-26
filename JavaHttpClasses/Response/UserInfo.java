import java.util.Date;

public class UserInfo
{
		private String UserName;
        private String DisplayName;
        private String Description;
        private String Email;
        private int ProductCount;
        private int FollowedUsers;
        private int FollowingUsers;
		private Date CreatedDate;
        private ImageInfo ProfilePicture;
		private int[] Products;
		
		public String getUserName()
		{
			return UserName;
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
		
		public int getProductCount()
		{
			return ProductCount;
		}
		
		public int getFollowedUsers()
		{
			return FollowedUsers;
		}
		
		public int getFollowingUsers()
		{
			return FollowingUsers;
		}
		
		public Date getCreatedDate()
		{
			return CreatedDate;
		}
		
		public ImageInfo getProfilePicture()
		{
			return ProfilePicture;
		}
		
		public int[] getProducts()
		{
			return Products;
		}
		
		public void setUserName(String UserName)
		{
			this.UserName = UserName;
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
		
		public void setProductCount(int ProductCount)
		{
			this.ProductCount = ProductCount;
		}
		
		public void setFollowedUsers(int FollowedUsers)
		{
			this.FollowedUsers = FollowedUsers;
		}
		
		public void setFollowingUsers(int FollowingUsers)
		{
			this.FollowingUsers = FollowingUsers;
		}
		
		public void setCreatedDate(Date CreatedDate)
		{
			this.CreatedDate = CreatedDate;
		}
		
		public void setProfilePicture(ImageInfo ProfilePicture)
		{
			this.ProfilePicture = ProfilePicture;
		}
		
		public void setProducts(int[] Products)
		{
			this.Products = Products;
		}
}