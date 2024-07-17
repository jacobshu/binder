
class Puzzle
{
  private string[,] State;
  private static int BoardSize = 4;
  private int MoveCount { get; set; }
  private (int X, int Y) OpenCursor { get; set; }
  private (int X, int Y) CurrentCursor { get; set; }
  private static string OpenMarker = "  ";

  public Puzzle()
  {
    State = CleanState();
    Randomize();
    FindOpenMarker();
    CurrentCursor = (0, 0);
  }

  private string[,] CleanState()
  {
    string[] source = new string[] { OpenMarker, " 1", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", "11", "12", "13", "14", "15" };

    int count = 0;
    string[,] newState = new string[BoardSize, BoardSize];
    for (int k = 0; k < BoardSize; k++)
    {
      for (int l = 0; l < BoardSize; l++)
      {
        if (source[count] == OpenMarker)
        {
          OpenCursor = (l, k);
        }
        newState[k, l] = source[count];
        count++;
      }
    }
    return newState;
  }

  private void Randomize()
  {
    Random rand = new Random();
    for (int i = 0; i < Math.Pow(BoardSize, BoardSize); i++)
    {
      int j = rand.Next(BoardSize);
      int k = i % BoardSize;
      int l = rand.Next(BoardSize);
      int m = k + 1 > 3 ? k - 1 : k + 1;
      (State[j, k], State[l, m]) = (State[l, m], State[j, k]);
    }
  }

  private void FindOpenMarker()
  {
    for (int k = 0; k < BoardSize; k++)
    {
      for (int l = 0; l < BoardSize; l++)
      {
        if (State[l, k] == OpenMarker)
        {
          OpenCursor = (k, l);
        }
      }
    }
  }

  private void CheckWinCondition()
  {
    string[,] winningState = new[,]{
      {"13", "14", "15", OpenMarker},
      {" 9", "10", "11", "12",},
      {" 5", " 6", " 7", " 8",} ,
      {" 1", " 2", " 3", " 4",} ,
    };

    if (State == winningState)
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("You win!");
    }
  }

  private string GetValue(int x, int y)
  {
    return State[y, x];
  }

  private void SetValue(int x, int y, string value)
  {
    State[y, x] = value;
  }

  private (int, int)[] ValidMoves()
  {
    (int x, int y) = OpenCursor;
    (int, int) filterOut = (-1, -1);
    (int, int) right = x < 3 ? (x + 1, y) : filterOut;
    (int, int) left = x > 0 ? (x - 1, y) : filterOut;
    (int, int) down = y > 0 ? (x, y - 1) : filterOut;
    (int, int) up = y < 3 ? (x, y + 1) : filterOut;

    (int, int)[] result = new[] { up, down, left, right };
    return Array.FindAll(result, i => i.Item1 >= 0);
  }

  private void GetMove()
  {
    do
    {
      ConsoleKey[] inputs = new[] { ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter, ConsoleKey.R };
      ConsoleKey key = Console.ReadKey().Key;
      if (!Array.Exists(inputs, k => k == key)) continue;
      switch (key)
      {
        case ConsoleKey.Enter:
          (int, int)[] moves = ValidMoves();
          if (Array.Exists(moves, m => m == CurrentCursor))
          {
            string val = GetValue(CurrentCursor.X, CurrentCursor.Y);
            SetValue(OpenCursor.X, OpenCursor.Y, val);
            SetValue(CurrentCursor.X, CurrentCursor.Y, OpenMarker);
            OpenCursor = (CurrentCursor.X, CurrentCursor.Y);
            MoveCount++;
          }
          break;
        case ConsoleKey.DownArrow:
          int upY = CurrentCursor.Y == 0 ? 0 : CurrentCursor.Y - 1;
          CurrentCursor = (CurrentCursor.X, upY);
          break;
        case ConsoleKey.UpArrow:
          int downY = CurrentCursor.Y == 3 ? 3 : CurrentCursor.Y + 1;
          CurrentCursor = (CurrentCursor.X, downY);
          break;
        case ConsoleKey.LeftArrow:
          int leftX = CurrentCursor.X == 0 ? 0 : CurrentCursor.X - 1;
          CurrentCursor = (leftX, CurrentCursor.Y);
          break;
        case ConsoleKey.RightArrow:
          int rightX = CurrentCursor.X == 3 ? 3 : CurrentCursor.X + 1;
          CurrentCursor = (rightX, CurrentCursor.Y);
          break;
        case ConsoleKey.R:
          State = CleanState();
          Randomize();
          FindOpenMarker();
          MoveCount = 0;
          break;
      }
      break;
    }
    while (true);
    Console.Clear();
  }

  public void Render()
  {
    (int, int)[] moves = ValidMoves();
    int iterator = 0;
    for (int i = State.GetLength(0) - 1; i >= 0; i--)
    {
      for (int j = 0; j < State.GetLength(1); j++)
      {
        if (Array.Exists(moves, t => t == (j, i)))
        {
          Console.ForegroundColor = ConsoleColor.Green;
        }
        if ((j, i) == CurrentCursor)
        {
          Console.BackgroundColor = ConsoleColor.Blue;
        }
        Console.Write(State[i, j] + " ");// + iterator + "\t");
        Console.ResetColor();
        iterator++;
      }
      Console.WriteLine();
    }
    Console.WriteLine($"Arrow Keys to move cursor. Press [Enter] to move a piece.");
    Console.WriteLine($"Press [R] to reset the board.");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write($"Moves: {MoveCount}");
    Console.ResetColor();
  }

  public void Run()
  {
    while (true)
    {
      Render();
      GetMove();
    }
  }
}
