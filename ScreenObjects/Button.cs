using System.Text;

namespace ConsoleMouse.ScreenObjects;

public class Button : Border
{

	public Label label;

	public Button(int x, int y, int w, int h, string label = "Button") : base(x, y, w, h)
	{

		int labelX = X + (W - 2 - label.Length) / 2 + 1;
		int labelY = Y + H / 2;

		this.label = new Label(labelX, labelY, label);

	}

	public void Update ()
	{
		base.Update();
		
		int labelX = X + (W - 2 - label.content.Length) / 2 + 1;
		int labelY = Y + H / 2;
		
		label.SetPos(labelX, labelY);
	}

	public virtual void Draw (Canvas canvas)
	{
		base.Draw(canvas);
		
		label.Draw(canvas);
	}
}