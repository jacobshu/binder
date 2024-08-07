public class Board
{
  public int BoardSize { get; }
  public Point CurrentCursor { get; set; }
  private Room[,] BoardState;
  public Game CurrentGame { get; }
  //    pit    ama   mael
  // sm   1      1      1  3/16,
  // md   2      2      1  5/36
  // lg   4      3      2  9/64

  public Board(int size, Game g)
  {
    BoardSize = size;
    CurrentGame = g;
    Random rand = new Random();
    Hazard[] hazards = CreateHazards(rand);

    CurrentCursor = new Point(0, 0);
    Room[,] newState = new Room[size, size];
    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size; j++)
      {
        newState[i, j] = new EmptyRoom(i, j, g);
      }
    }
    BoardState = PlaceSpecialRooms(newState, hazards, rand);
  }

  public Board GetBoard() { return this; }

  public Room RoomAt(int x, int y)
  {
    return BoardState[x, y];
  }

  public Room[] GetAdjacentRooms(int x, int y)
  {
    List<(int, int)> coords = new List<(int, int)> { };
    /*int count = 0;*/
    for (int i = -1; i <= 1; i++)
    {
      for (int j = -1; j <= 1; j++)
      {
        int rx = Math.Clamp(x + i, 0, BoardSize - 1);
        int ry = Math.Clamp(y + j, 0, BoardSize - 1);
        coords.Add((rx, ry));
        /*coords[count] = (rx, ry);*/
      }
    }

    var dedup = coords.Distinct().ToList();
    Room[] result = new Room[dedup.Count];
    for (int i = 0; i < result.Length; i++)
    {
      result[i] = RoomAt(dedup[i].Item1, dedup[i].Item2);
      /*if (Globals.DEBUG) Console.WriteLine(dedup[i].ToString());*/
    }

    return result;
  }

  public void Render()
  {
    Room[] adjacentRooms = GetAdjacentRooms(CurrentCursor.X, CurrentCursor.Y);
    Room[] nonEmptyRooms = Enumerable.Where(adjacentRooms, r => !(r is EmptyRoom)).ToArray();

    if (Globals.DEBUG)
    {
      bool isAtVerticalEdge = CurrentCursor.Y == 0 || CurrentCursor.Y == BoardSize - 1;
      int linesPrinted = 0;
      for (int i = 0; i < adjacentRooms.Length; i++)
      {
        Console.Write($"{adjacentRooms[i].ToString()} ");
        // 2 room width column at the vertical edges, otherwise 3 columns
        int index = i + 1;
        if (index % (isAtVerticalEdge ? 2 : 3) == 0)
        {
          Console.WriteLine();
          linesPrinted++;
        }
      }
      // ensure 3 lines so board doesn't shift
      if (linesPrinted == 2) Console.WriteLine();
      Console.WriteLine();

    }

    List<(string, ConsoleColor)> descriptions = new List<(string, ConsoleColor)> { };
    foreach (Room ar in nonEmptyRooms)
    {
      (string nearbyDesc, ConsoleColor nearbyColor) = ar.Nearby();
      bool isInList = descriptions.Contains((nearbyDesc, nearbyColor));
      if (isInList) continue;
      descriptions.Add((nearbyDesc, nearbyColor));
    }

    Border(BorderSide.Top, ConsoleColor.DarkGreen);
    Console.WriteLine();

    int iterator = 0;
    int line = 0;
    (string enterDesc, ConsoleColor enterColor) = ("", ConsoleColor.Gray);
    for (int i = 0; i < BoardState.GetLength(0); i++)
    {
      Border(BorderSide.Side, ConsoleColor.DarkGreen);
      for (int j = 0; j < BoardState.GetLength(1); j++)
      {
        bool isCurrentCursor = CurrentCursor.X == i && CurrentCursor.Y == j;
        if (isCurrentCursor) RoomAt(i, j).Enter();
        RoomAt(i, j).Render(isCurrentCursor);
        Console.ResetColor(); // • 
        iterator++;
      }
      Border(BorderSide.Side, ConsoleColor.DarkGreen);

      // narrations
      if (descriptions.Count > 0 && line < descriptions.Count)
      {
        Console.ForegroundColor = descriptions[line].Item2;
        Console.Write(descriptions[line].Item1);
        Console.ResetColor();
      }
      Console.WriteLine();
      line++;
    }
    Border(BorderSide.Bottom, ConsoleColor.DarkGreen);

    Console.WriteLine();

    Console.WriteLine($"Use the arrow keys [ ▲ ▼ ◀ ▶ ] to move.");
    Console.WriteLine($"[R]eset the board. [Q]uit the game.");
    Console.ResetColor();
  }

  public void Render(Message msg)
  {
    Console.Clear();
    Border(BorderSide.Top, msg.Color);
    Console.WriteLine();

    int line = 0;
    for (int i = 0; i < BoardState.GetLength(0); i++)
    {
      Border(BorderSide.Side, msg.Color);
      for (int j = 0; j < BoardState.GetLength(1); j++)
      {
        bool isCurrentCursor = CurrentCursor.X == i && CurrentCursor.Y == j;
        RoomAt(i, j).Render(isCurrentCursor);
      }
      Border(BorderSide.Side, msg.Color);
      
      if (line == 0) Console.Write(" " + msg.Text); 
      Console.WriteLine();
      line++;
    }
    Border(BorderSide.Bottom, msg.Color);

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

      // no hazards in the 2x2 starting area
      if (x == 0 && y == 0 || x == 1 && y == 0 || x == 1 && y == 1 || x == 0 && y == 1) continue;

      // don't copy over previous hazards
      if (state[x, y] is PitRoom || state[x, y] is AmarokRoom || state[x, y] is MaelstromRoom) continue;

      if (i >= hazards.Length)
      {
        if (Globals.DEBUG) Console.WriteLine($"iteration: {i}, fountain room");

        // place the fountain in the second half of the board (both axes)
        int newX = rand.Next(BoardSize / 2, BoardSize);
        int newY = rand.Next(BoardSize / 2, BoardSize);
        if (
          state[newX, newY] is PitRoom ||
          state[newX, newY] is AmarokRoom ||
          state[newX, newY] is MaelstromRoom
        ) continue;

        state[newX, newY] = new FountainRoom(newX, newY, CurrentGame);
      }
      else
      {
        if (Globals.DEBUG) Console.WriteLine($"iteration: {i}, hazard room");
        switch (hazards[i])
        {
          case Hazard.Pit:
            state[x, y] = new PitRoom(x, y, CurrentGame);
            break;
          case Hazard.Amarok:
            state[x, y] = new AmarokRoom(x, y, CurrentGame);
            break;
          case Hazard.Maelstrom:
            state[x, y] = new MaelstromRoom(x, y, CurrentGame);
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

  public void Border(BorderSide side, ConsoleColor color)
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
