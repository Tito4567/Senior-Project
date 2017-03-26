public class GetFollowingFeedResponse extends LacesResponse
{
	private FeedItem[] Products;
	
	public FeedItem[] getProducts()
	{
		return Products;
	}
	
	public void setProducts(FeedItem[] Products)
	{
		this.Products = Products;
	}
}