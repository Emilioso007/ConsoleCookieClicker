
namespace ConsoleMouse.ScreenObjects;

public class Label
{
	private int x, y;
	public string Content;
	
	public Label(int x, int y, string content = "Label")
	{
		this.x = x;
		this.y = y;
		this.Content = content;
	}

	public void Draw (Canvas canvas)
	{
		canvas.AddContent(Content, x, y);
	}

	public void SetPos(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}