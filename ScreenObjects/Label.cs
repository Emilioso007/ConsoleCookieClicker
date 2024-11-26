
namespace ConsoleMouse.ScreenObjects;

public class Label
{
	private int x, y;
	public string content;
	
	public Label(int x, int y, string content = "Label")
	{
		this.x = x;
		this.y = y;
		this.content = content;
	}

	public void Draw (Canvas canvas)
	{
		canvas.AddContent(content, x, y);
	}

	public void SetPos(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}