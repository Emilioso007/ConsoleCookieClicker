
namespace ConsoleMouse.ScreenObjects;

public class Canvas
{
	public int W => canvas.GetLength(0);

	public int H => canvas.GetLength(1);

	private char[,] canvas;
	public Canvas()
	{
		Reset();
	}

	private void Reset ()
	{
		canvas = new char[Console.WindowWidth, Console.WindowHeight-1];

		// Initialize the canvas with spaces
		for (int i = 0; i < W; i++)
		{
			for (int j = 0; j < H; j++)
			{
				canvas[i, j] = ' ';
			}
		}
	}

	public void AddContent(string value, int x, int y)
	{
		if (x < 0 || x >= canvas.GetLength(0) || y < 0 || y >= canvas.GetLength(1))
		{
			throw new ArgumentOutOfRangeException("The starting coordinates are outside the canvas bounds.");
		}

		// Split the input string into lines
		string[] lines = value.Split('\n');

		for (int i = 0; i < lines.Length; i++)
		{
			// Get the current line
			string line = lines[i];

			// Write the line onto the canvas, character by character
			for (int j = 0; j < line.Length; j++)
			{
				int targetX = x + j;
				int targetY = y + i;

				// Check bounds before adding
				if (targetX < canvas.GetLength(0) && targetY < canvas.GetLength(1))
				{
					canvas[targetX, targetY] = line[j];
				}
			}
		}
	}

	public void Show()
	{
		// Build the canvas representation as a string
		var builder = new System.Text.StringBuilder();
		for (int j = 0; j < H; j++) // Row-major order
		{
			for (int i = 0; i < W; i++)
			{
				builder.Append(canvas[i, j]);
			}
			builder.AppendLine(); // Add a newline after each row
		}
		Console.SetCursorPosition(0,0);
		Console.Write(builder.ToString());
		
		Reset();
		
	}
}
