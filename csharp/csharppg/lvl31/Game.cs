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
      switch (key)
      {
        /*case ConsoleKey.Enter:*/
        /*  break;*/
        /*case ConsoleKey.DownArrow:*/
        /*  break;*/
        /*case ConsoleKey.UpArrow:*/
        /*  break;*/
        /*case ConsoleKey.LeftArrow:*/
        /*  break;*/
        /*case ConsoleKey.RightArrow:*/
        /*  break;*/
        /*case ConsoleKey.R:*/
        /*  break;*/
        case ConsoleKey.Q:
          Console.Clear();
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("Come back");
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
