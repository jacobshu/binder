public class Board
{
  private int BoardSize { get; }
  private Point CurrentCursor { get; }
  private Room[,] BoardState;
  //    pit    ama   mael
  // sm   1      1      1  4
  // md   2      2      1  6
  // lg   4      3      2  8

  public Board(int size)
  {
    Random rand = new Random();
    CurrentCursor = new Point(0, 0);
    Room[,] newState = new Room[size, size];
    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size; j++)
      {
        newState[i, j] = new EmptyRoom(i, j);
      }
    }
    BoardState = newState;
    BoardSize = size;
  }

  public Room RoomAt(int x, int y)
  {
    return BoardState[x, y];
  }

  public void Render()
  {
    Console.WriteLine($"board size is {BoardSize}");
    Border(BorderSide.Top, ConsoleColor.DarkGreen);
    Console.WriteLine();

    int iterator = 0;
    for (int i = 0; i < BoardState.GetLength(0); i++)
    {
      Border(BorderSide.Side, ConsoleColor.DarkGreen);
      for (int j = 0; j < BoardState.GetLength(1); j++)
      {
        /*if (CurrentCursor.X == i && CurrentCursor.Y == j)*/
        /*{*/
        /*  Console.BackgroundColor = ConsoleColor.Cyan;*/
        /*}*/
        RoomAt(i, j).Render();
        iterator++;
      }
      Border(BorderSide.Side, ConsoleColor.DarkGreen);
      Console.WriteLine();
    }
    Border(BorderSide.Bottom, ConsoleColor.DarkGreen); 
    Console.WriteLine();

    Console.WriteLine($"Arrow Keys to move cursor. Press [Enter] to move a piece.");
    Console.WriteLine($"Press [R] to reset the board.");
    Console.ResetColor();
  }

  private void Border(BorderSide side, ConsoleColor color)
  {
    string tl = "┏";
    string vl = "┃";
    string br = "┛";
    string bl = "┗";
    string hl = "━";
    string tr = "┓";

    Console.ForegroundColor = color;
    switch (side)
    {
      case BorderSide.Side:
        Console.Write(vl);
        break;
      case BorderSide.Top:
        Console.Write(tl);
        Console.Write("   ");
        IEnumerable<string> tHorizontal = Enumerable.Repeat(hl, BoardSize * 2 + 1);
        foreach (string str in tHorizontal)
        {
          Console.Write(str);
        }
        Console.Write(tr);
        break;
      case BorderSide.Bottom:
        Console.Write(bl);
        IEnumerable<string> bHorizontal = Enumerable.Repeat(hl, BoardSize * 3);
        foreach (string str in bHorizontal)
        {
          Console.Write(str);
        }
        Console.Write(br);
        break;
    }
    Console.ResetColor();
  }
}

public enum BorderSide
{
  Top,
  Side,
  Bottom,
}
