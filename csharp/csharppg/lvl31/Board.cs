public class Board
{
  private int BoardSize { get; }
  private Point CurrentCursor { get; }
  private Room[,] BoardState;
  //    pit    ama   mael
  // sm   1      1      1  3/16,
  // md   2      2      1  5/36
  // lg   4      3      2  9/64

  public Board(int size)
  {
    Random rand = new Random();
    int hazards = BoardSize >= 8 ? BoardSize - 1 : BoardSize + 1;
    int pits = (int)Math.Round(2 / 25 * Math.Pow(BoardSize, 2));
    int amaroks = (int)Math.Round((double)(hazards - pits) / 2);
    int maelstroms = hazards - pits - amaroks > 0 ? hazards - pits - amaroks : 0;
    Hazard[] hazardList = new Hazard[hazards];
    for (int i = 0; i < hazards; i++)
    {
      if (i < pits - 1)
      {
        hazardList[i] = Hazard.Pit;
      }
      else if (i < pits + amaroks - 1)
      {
        hazardList[i] = Hazard.Amarok;
      }
      else
      {
        hazardList[i] = Hazard.Maelstrom;
      }
    }
    rand.Shuffle(hazardList);

    CurrentCursor = new Point(0, 0);
    Room[,] newState = new Room[size, size];
    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size; j++)
      {
        bool hasHazard = rand.Next(100) < 20;
        if (hasHazard)
        {
          switch (hazardList[hazards - 1])
          {
            case Hazard.Pit:
            newState[i, j] = new PitRoom(i, j);
            break;
            case Hazard.Amarok:
              newState[i, j] = new AmarokRoom(i, j);
              break;
            case Hazard.Maelstrom:
              newState[i, j] = new MaelstromRoom(i, j);
              break;
          }
          /*hazards -= 1;*/
        }
        else
        {
          newState[i, j] = new EmptyRoom(i, j);
        }
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

    Console.WriteLine($"Use the arrow keys [ ▲ ▼ ◀ ▶ ] to move.");
    Console.WriteLine($"[R]eset the board. [Q]uit the game.");
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
