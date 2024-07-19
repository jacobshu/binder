public static class Globals
{
  public const bool DEBUG = true;
}

public class Game
{
  public Board GameBoard;

  public Game(int size)
  {
    GameBoard = new Board(size);
  }

  private void HandleInput()
  {
    do
    {
      ConsoleKey[] validInputs = new[] { 
        ConsoleKey.LeftArrow, 
        ConsoleKey.RightArrow, 
        ConsoleKey.UpArrow, 
        ConsoleKey.DownArrow, 
        ConsoleKey.Enter, 
        ConsoleKey.R, 
        ConsoleKey.Q, 
      };
      ConsoleKey key = Console.ReadKey().Key;
      if (!Array.Exists(validInputs, k => k == key)) continue;
      Point cc = GameBoard.CurrentCursor;
      switch (key)
      {
        /*case ConsoleKey.Enter:*/
        /*  break;*/
        case ConsoleKey.DownArrow:
          int downX = cc.X + 1 > GameBoard.BoardSize - 1 ? GameBoard.BoardSize - 1 : cc.X + 1;
          GameBoard.CurrentCursor = new Point(downX, cc.Y);
          break;
        case ConsoleKey.UpArrow:
          int upX = cc.X - 1 > GameBoard.BoardSize - 1 ? GameBoard.BoardSize - 1 : cc.X - 1;
          GameBoard.CurrentCursor = new Point(upX, cc.Y);
          break;
        case ConsoleKey.LeftArrow:
          int leftY = cc.Y - 1 > GameBoard.BoardSize - 1 ? GameBoard.BoardSize - 1 : cc.Y - 1;
          GameBoard.CurrentCursor = new Point(cc.X, leftY);
          break;
        case ConsoleKey.RightArrow:
          int rightY = cc.Y + 1 > GameBoard.BoardSize - 1 ? GameBoard.BoardSize - 1 : cc.Y + 1;
          GameBoard.CurrentCursor = new Point(cc.X, rightY);
          break;
        case ConsoleKey.R:
          Console.Clear();
          int size = GameBoard.BoardSize;
          GameBoard = new Board(size);
          break;
        case ConsoleKey.Q:
          Console.Clear();
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("The fountain awaits...");
          Environment.Exit(0);
          break;
      }
      break;
    }
    while (true);
    Console.Clear();
  }

  public void Run()
  {
    while (true)
    {
      GameBoard.Render();
      HandleInput();
    }
  }
}
