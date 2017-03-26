public class ProcessTransactionRequest extends ProductRequest
{
	private int BuyerId;
	private int SellerId;
	private String ReferenceNumber;
	private double Amount;
	
	public int getBuyerId()
	{
		return BuyerId;
	}
	
	public int getSellerId()
	{
		return SellerId;
	}
	
	public String getReferenceNumber()
	{
		return ReferenceNumber;
	}
	
	public double getAmount()
	{
		return Amount;
	}
	
	public void setBuyerId(int BuyerId)
	{
		this.BuyerId = BuyerId;
	}
	
	public void setSellerId(int SellerId)
	{
		this.SellerId = SellerId;
	}
	
	public void setReferenceNumber(String ReferenceNumber)
	{
		this.ReferenceNumber = ReferenceNumber;
	}
	
	public void setAmount(double Amount)
	{
		this.Amount = Amount;
	}
}