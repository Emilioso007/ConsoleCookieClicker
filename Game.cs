using ConsoleMouse.ScreenObjects;

namespace ConsoleMouse;
public class Game
{
	private Canvas canvas;
	
	private DateTime previous;
	
	private (int w, int h) conDim;
	private Dictionary<string, Button> buttons;
	private Dictionary<string, Label> labels;
	private Dictionary<string, Upgrade> upgrades;

	public bool running = true;

	private int cookieCount = 0;
	public Game ()
	{
		previous = DateTime.Now;
		conDim = (Console.WindowWidth, Console.WindowHeight-1);
		canvas = new Canvas();
		buttons = new Dictionary<string, Button>();
		labels = new Dictionary<string, Label>();
		upgrades = new Dictionary<string, Upgrade>();
		
		buttons.Add("ExitButton", new Button(conDim.w - 6, 0, 6, 3, "Exit"));
		buttons.Add("CookieButton", new Button(2, 5, 20, 11, "Cookie"));
		
		labels.Add("CookieCountLabel", new Label(2, 2, "Cookies: #######"));
		labels.Add("CookiesPerSecondLabel", new Label(2, 4, "CPS: #######"));
		
		upgrades.Add("CursorUpgrade", new Upgrade(conDim.w-20, 5,20,5,"Cursor",15,1));
		upgrades.Add("GrandmaUpgrade", new Upgrade(conDim.w-20, 10,20,5,"Grandma",100,10));

	}

	public void Update ()
	{
		
		foreach (Button b in buttons.Values)
		{
			b.Update();
		}

		foreach (string key in buttons.Keys)
		{
			if (buttons[key].Clicked)
			{
				switch (key)
				{
					case "ExitButton":
						running = false;
						break;
					case "CookieButton":
						cookieCount++;
						break;
				}		
			}
		}
		
		foreach (Upgrade u in upgrades.Values)
		{
			u.Update();
		}
		
		foreach (string key in upgrades.Keys)
		{
			if (upgrades[key].Clicked)
			{
				cookieCount -= upgrades[key].TryPurchase(cookieCount);
			}
		}

		if (DateTime.Now.Subtract(previous).Seconds >= 1)
		{
			previous = DateTime.Now;
			cookieCount += CalculateCPS();
		}

		labels["CookieCountLabel"].content = "Cookies: " + cookieCount;
		labels["CookiesPerSecondLabel"].content = "CPS: " + CalculateCPS();

	}

	public void Show ()
	{
		
		foreach (Button b in buttons.Values)
		{
			b.Draw(canvas);
		}
		foreach (Upgrade u in upgrades.Values)
		{
			u.Draw(canvas);
		}
		foreach (Label l in labels.Values)
		{
			l.Draw(canvas);
		}
		
		canvas.Show();

	}

	private int CalculateCPS ()
	{
		int result = 0;
		foreach (Upgrade u in upgrades.Values)
		{
			result += u.GetAmountPerSecond() * u.GetAmount();
		}
		return result;
	}

}