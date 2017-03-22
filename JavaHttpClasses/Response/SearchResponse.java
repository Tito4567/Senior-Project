public class SearchResponse extends LacesResponse
{
	private int[] Users;
	private int[] Products;
	
	public int[] getUsers()
	{
		return Users;
	}
	
	public int[] getProducts()
	{
		return Products;
	}
	
	public void setUsers(int[] Users)
	{
		this.Users = Users;
	}
	
	public void setProducts(int[] Products)
	{
		this.Products = Products;
	}
}