
namespace ConsoleMouse;

public class Program {
	
	public static void Main(string[] args)
	{
		Console.CursorVisible = false;

		Game game = new Game();
		
		Mouse.Initialize();
		while (game.running)
		{
			Mouse.Update();
			game.Update();
			game.Show();
			Thread.Sleep(1000/60);
		}
	}
}
