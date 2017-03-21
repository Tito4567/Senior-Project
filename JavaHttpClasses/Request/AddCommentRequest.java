public class AddCommentRequest extends ProductRequest
{
	private String Text;
	
	public String getText()
	{
		return Text;
	}
	
	public void setText(String Text)
	{
		this.Text = Text;
	}
}