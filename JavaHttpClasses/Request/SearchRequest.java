public class SearchRequest extends LacesRequest
{
	private int SearchType;
	private String[] Keywords;
	private String[] Tags;
	private double MinPrice;
	private double MaxPrice;
	private String Size;
	private String Brand;
	
	public int getSearchType()
	{
		return SearchType;
	}
	
	public String[] getKeywords()
	{
		return Keywords;
	}
	
	public String[] getTags()
	{
		return Tags;
	}
	
	public double getMinPrice()
	{
		return MinPrice;
	}
	
	public double getMaxPrice()
	{
		return Max;
	}
	
	public String getSize()
	{
		return Size;
	}
	
	public String getBrand()
	{
		return Brand;
	}
	
	public void setSearchType(int SearchType)
	{
		this.SearchType = SearchType;
	}
	
	public void setKeywords(String[] Keywords)
	{
		this.Keywords = Keywords;
	}
	
	public void setTags(String[] Tags)
	{
		this.Tags = Tags;
	}
	
	public void setMinPrice(double MinPrice)
	{
		this.MinPrice = MinPrice;
	}
	
	public void setMaxPrice(double MaxPrice)
	{
		this.MaxPrice = MaxPrice;
	}
	
	public void setSize(String Size)
	{
		this.Size = Size;
	}
	
	public void setBrand(String Brand)
	{
		this.Brand = Brand;
	}
}