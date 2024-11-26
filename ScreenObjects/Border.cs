
namespace ConsoleMouse.ScreenObjects;

public class Border
{

	public int X, Y, W, H;
	public bool Clicked;

	public Border(int x, int y, int w, int h)
	{
		X = x;
		Y = y;
		W = w;
		H = h;
		Clicked = false;
	}

	public virtual void Update ()
	{
		if (Mouse.X >= X && Mouse.X <= X + W && Mouse.Y >= Y && Mouse.Y <= Y + H)
		{
			if (Mouse.LEFT)
			{
				Clicked = true;
			}
			else
			{
				Clicked = false;
			}
		}
		else
		{
			Clicked = false;
		}
	}

	public void Draw (Canvas canvas)
	{
		string toAdd = "";

		toAdd += "╔" + new string('═', W - 2) + "╗\n";

		for (int i = 0 ; i < H - 2 ; i++)
		{
			toAdd += "║" + new string(' ', W - 2) + "║\n";
		}
		
		toAdd += "╚" + new string('═', W - 2) + "╝";
		
		canvas.AddContent(toAdd, X, Y);
	}

}