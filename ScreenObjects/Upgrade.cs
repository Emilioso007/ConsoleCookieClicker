
namespace ConsoleMouse.ScreenObjects;

public class Upgrade : Button
{

	private string name;
	private int baseCost;

	private int currentCost
	{
		get
		{
			return (int)Math.Ceiling(baseCost * Math.Pow(1.15, count));
		}
	}
	private int scorePerSecond;
	private int count;

	public Upgrade(int x, int y, int w, int h, string name, int baseCost, int scorePerSecond) : base(x, y, w, h)
	{
		this.name = name;
		this.baseCost = baseCost;
		this.scorePerSecond = scorePerSecond;
		count = 0;

		label.content = name + ": " + currentCost + "$ <" + count + ">";
	}

	public override void Draw (Canvas canvas)
	{
		label.content = name + ": " + currentCost + "$ <" + count + ">";
		base.Draw(canvas);
	}

	public int TryPurchase(int cookieCount)
	{
		if (cookieCount < currentCost) return 0;

		int toReturn = currentCost;
		count++;
		return toReturn;
	}

	public int GetCost ()
	{
		return currentCost;
	}

	public void AddAmount(int amount)
	{
		count += amount;
	}
	
	public int GetAmountPerSecond ()
	{
		return scorePerSecond;
	}

	public int GetAmount ()
	{
		return count;
	}
}