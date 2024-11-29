// Game.cs
using ConsoleMouse.ScreenObjects;

namespace ConsoleMouse;
public class Game
{
    private Canvas canvas;
    private DateTime previous;
    private readonly (int w, int h) conDim;
    private Dictionary<string, Button> buttons;
    private Dictionary<string, Label> labels;
    private Dictionary<string, Upgrade> upgrades;

    public bool Running { get; private set; } = true;
    private int cookieCount = 0;

    public Game()
    {
        previous = DateTime.Now;
        conDim = (Console.WindowWidth, Console.WindowHeight - 1);
        canvas = new Canvas();
        buttons = new Dictionary<string, Button>();
        labels = new Dictionary<string, Label>();
        upgrades = new Dictionary<string, Upgrade>();

        buttons.Add("ExitButton", new Button(conDim.w - 6, 0, 6, 3, "Exit"));
        buttons.Add("CookieButton", new Button(2, 5, 20, 11, "Cookie"));

        labels.Add("CookieCountLabel", new Label(2, 2, "Cookies: #######"));
        labels.Add("CookiesPerSecondLabel", new Label(2, 4, "CPS: #######"));

        upgrades.Add("CursorUpgrade", new Upgrade(conDim.w - 20, 5, 20, 5, "Cursor", 15, 1));
        upgrades.Add("GrandmaUpgrade", new Upgrade(conDim.w - 20, 10, 20, 5, "Grandma", 100, 10));
        upgrades.Add("FarmUpgrade", new Upgrade(conDim.w - 20, 15, 20, 5, "Farm", 500, 250));
        upgrades.Add("MineUpgrade", new Upgrade(conDim.w - 20, 20, 20, 5, "Mine", 2000, 1000));
    }

    public void Update()
    {
        HandleButtonClicks();
        UpdateUpgrades();

        if ((DateTime.Now - previous).TotalSeconds >= 1)
        {
            previous = DateTime.Now;
            cookieCount += CalculateCPS();
        }

        labels["CookieCountLabel"].Content = "Cookies: " + cookieCount;
        labels["CookiesPerSecondLabel"].Content = "CPS: " + CalculateCPS();
    }

    private void HandleButtonClicks()
    {
        foreach (var button in buttons.Values)
        {
            button.Update();
        }

        if (buttons["ExitButton"].Clicked)
        {
            Running = false;
        }

        if (buttons["CookieButton"].Clicked)
        {
            cookieCount++;
        }
    }

    private void UpdateUpgrades()
    {
        foreach (var upgrade in upgrades.Values)
        {
            upgrade.Update();
        }

        foreach (var key in upgrades.Keys)
        {
            if (upgrades[key].Clicked)
            {
                cookieCount -= upgrades[key].TryPurchase(cookieCount);
            }
        }
    }

    public void Show()
    {
        foreach (var button in buttons.Values)
        {
            button.Draw(canvas);
        }
        foreach (var upgrade in upgrades.Values)
        {
            upgrade.Draw(canvas);
        }
        foreach (var label in labels.Values)
        {
            label.Draw(canvas);
        }

        canvas.Show();
    }

    private int CalculateCPS()
    {
        return upgrades.Values.Sum(u => u.GetAmountPerSecond() * u.GetAmount());
    }
}