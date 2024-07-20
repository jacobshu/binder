public class Board
{
  public int BoardSize { get; }
  public Point CurrentCursor { get; set; }
  private Room[,] BoardState;
  //    pit    ama   mael
  // sm   1      1      1  3/16,
  // md   2      2      1  5/36
  // lg   4      3      2  9/64

  public Board(int size)
  {
    BoardSize = size;
    Random rand = new Random();
    Hazard[] hazards = CreateHazards(rand);

    CurrentCursor = new Point(0, 0);
    Room[,] newState = new Room[size, size];
    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size; j++)
      {
        newState[i, j] = new EmptyRoom(i, j);
      }
    }
    BoardState = PlaceSpecialRooms(newState, hazards, rand);
  }

  public Room RoomAt(int x, int y)
  {
    return BoardState[x, y];
  }

  public Room[] GetAdjacentRooms(int x, int y)
  {
    Room[] adj = new Room[9];
    int count = 0;
    for (int i = -1; i <= 1; i++)
    {
      for (int j = -1; j <= 1; j++)
      {
        int rx = Math.Clamp(x + i, 0, BoardSize - 1);
        int ry = Math.Clamp(y + j, 0, BoardSize - 1);
        if (Globals.DEBUG) Console.WriteLine($"check adjacent room at: {rx}, {ry}, with i: {i}, j: {j}");
        adj[count] = RoomAt(rx, ry);
      }
    }
    return adj;
  }

  public void Render()
  {
    Room[] adjacentRooms = GetAdjacentRooms(CurrentCursor.X, CurrentCursor.Y);
    Console.WriteLine($"{adjacentRooms.Length} rooms: {adjacentRooms}");
    foreach (Room r in adjacentRooms)
    {
      Console.WriteLine($"empty: {r is EmptyRoom}, pit: {r is PitRoom}, amarok: {r is AmarokRoom}, maelstrom: {r is MaelstromRoom}");
    }
    /*var nonEmptyRooms = Enumerable.Where(adjacentRooms, r => !(r is EmptyRoom));*/

    Border(BorderSide.Top, ConsoleColor.DarkGreen);
    Console.WriteLine();
    foreach (Room ar in adjacentRooms)
    {
      (string nearbyDesc, ConsoleColor nearbyColor) = ar.Nearby();
      Console.ForegroundColor = nearbyColor;
      Console.Write(nearbyDesc);
      Console.ResetColor();
    }
    
    int iterator = 0;
    (string enterDesc, ConsoleColor enterColor) = ("", ConsoleColor.Gray);
    for (int i = 0; i < BoardState.GetLength(0); i++)
    {
      Border(BorderSide.Side, ConsoleColor.DarkGreen);
      for (int j = 0; j < BoardState.GetLength(1); j++)
      {
        bool isCurrentCursor = CurrentCursor.X == i && CurrentCursor.Y == j;
        if (isCurrentCursor) (enterDesc, enterColor) = RoomAt(i, j).Enter();
        RoomAt(i, j).Render(isCurrentCursor);
        Console.ResetColor(); // • 
        iterator++;
      }
      Border(BorderSide.Side, ConsoleColor.DarkGreen);
       
      // write narrations[i] here

      Console.WriteLine();
    }
    Border(BorderSide.Bottom, ConsoleColor.DarkGreen);
    Console.WriteLine();

    Console.WriteLine($"Use the arrow keys [ ▲ ▼ ◀ ▶ ] to move.");
    Console.WriteLine($"[R]eset the board. [Q]uit the game.");
    Console.ResetColor();
  }

  private Hazard[] CreateHazards(Random rand)
  {
    int pits = (int)Math.Round(0.75 * BoardSize - 2.25);
    int amaroks = (int)Math.Round(0.5 * BoardSize - 1);
    int maelstroms = (int)Math.Round(0.25 * BoardSize - 0.25);
    int hazards = pits + amaroks + maelstroms;
    Hazard[] hazardList = new Hazard[hazards];
    for (int i = 0; i < hazards; i++)
    {
      if (i < pits)
      {
        hazardList[i] = Hazard.Pit;
      }
      else if (i < pits + amaroks)
      {
        hazardList[i] = Hazard.Amarok;
      }
      else
      {
        hazardList[i] = Hazard.Maelstrom;
      }
    }
    rand.Shuffle(hazardList);
    rand.Shuffle(hazardList);

    if (Globals.DEBUG)
    {
      Console.WriteLine($"There are {hazards} hazards comprising: ");
      Console.WriteLine($"pits {pits}, amaroks: {amaroks}, maelstroms: {maelstroms}");
      foreach (Hazard h in hazardList)
      {
        Console.WriteLine($"{h}");
      }
    }
    return hazardList;
  }

  private Room[,] PlaceSpecialRooms(Room[,] state, Hazard[] hazards, Random rand)
  {
    int i = 0;
    while (i < hazards.Length + 1)
    {
      int x = rand.Next(BoardSize);
      int y = rand.Next(BoardSize);
      if (x == 0 && y == 0) continue;
      if (state[x, y] is PitRoom || state[x, y] is AmarokRoom || state[x, y] is MaelstromRoom) continue;
      
      if (i >= hazards.Length)
      {
        if (Globals.DEBUG) Console.WriteLine($"iteration: {i}, fountain room");
        int newX = rand.Next(BoardSize / 2, BoardSize);
        int newY = rand.Next(BoardSize / 2, BoardSize);
        if (state[newX, newY] is PitRoom || state[newX, newY] is AmarokRoom || state[newX, newY] is MaelstromRoom) continue;
        state[newX, newY] = new FountainRoom(newX, newY);
      } else {
        if (Globals.DEBUG) Console.WriteLine($"iteration: {i}, hazard room");
        switch (hazards[i])
        {
          case Hazard.Pit:
            state[x, y] = new PitRoom(x, y);
            break;
          case Hazard.Amarok:
            state[x, y] = new AmarokRoom(x, y);
            break;
          case Hazard.Maelstrom:
            state[x, y] = new MaelstromRoom(x, y);
            break;
        }
      }
      i++;
    }
    return state;
  }

  private Point GetPotentialPoint(Random rand)
  {
    return new Point(rand.Next(BoardSize), rand.Next(BoardSize));
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
        IEnumerable<string> tHorizontal = Enumerable.Repeat(hl, BoardSize * 3 - 3);
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
