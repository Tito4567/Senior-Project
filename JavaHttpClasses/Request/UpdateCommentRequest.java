public class UpdateCommentRequest extends CommentRequest
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