public abstract class Room
{
  public Hazard? RoomHazard { get; }
  private ConsoleColor Color { get; }
  private Point RoomCursor { get; }

  public Room(int x, int y, Hazard? hazard)
  {
    RoomHazard = hazard;
    RoomCursor = new Point(x, y);
  }

  public abstract void Render(bool isCurrentCursor);

  public bool IsEmpty() => RoomHazard == null;

  public abstract (string, ConsoleColor) Nearby();
  public abstract (string, ConsoleColor) Enter();
}

public class EmptyRoom : Room
{
  public EmptyRoom(int x, int y) : base(x, y, null) { }

  public override void Render(bool isCurrentCursor)
  {

    if (!isCurrentCursor)
    {
      Console.Write(" · ");
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.Write(" • ");
      Console.ResetColor();
    }
  }

  public override (string, ConsoleColor) Enter()
  {
    return ("You see nothing of note.", ConsoleColor.Gray);
  }

  public override (string, ConsoleColor) Nearby()
  {
    return ("", ConsoleColor.Gray);
  }
}

public class FountainRoom : Room
{
  public FountainRoom(int x, int y) : base(x, y, null) { }
  public override void Render(bool isCurrentCursor)
  {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(" Θ ");
    Console.ResetColor();
  }

  public override (string, ConsoleColor) Enter()
  {
    return ("The fountain glitters before you!", ConsoleColor.Yellow);
  }

  public override (string, ConsoleColor) Nearby()
  {
    return ("You hear water dripping nearby...", ConsoleColor.Yellow);
  }
}

public class PitRoom : Room
{
  public PitRoom(int x, int y) : base(x, y, Hazard.Pit) { }
  public override void Render(bool isCurrentCursor)
  {
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write(" O ");
    Console.ResetColor();
  }

  public override (string, ConsoleColor) Enter()
  {
    return ("You feel a draft coming from nearby.", ConsoleColor.Black);
  }

  public override (string, ConsoleColor) Nearby()
  {
    return ("You fall to your demise.", ConsoleColor.Black);
  }
}

public class MaelstromRoom : Room
{
  public MaelstromRoom(int x, int y) : base(x, y, Hazard.Maelstrom) { }
  public override void Render(bool isCurrentCursor)
  {
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(" § ");
    Console.ResetColor();
  }

  public override (string, ConsoleColor) Enter()
  {
    return ("You are taken up in a maelstrom!", ConsoleColor.Blue);
  }

  public override (string, ConsoleColor) Nearby()
  {
    return ("You hear the growling of a maelstrom nearby", ConsoleColor.Gray);
  }
}

public class AmarokRoom : Room
{
  public AmarokRoom(int x, int y) : base(x, y, Hazard.Amarok) { }
  public override void Render(bool isCurrentCursor)
  {
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write(" Ψ ");
    Console.ResetColor();
  }

  public override (string, ConsoleColor) Enter()
  {
    return ("You are torn asunder by an amarok!", ConsoleColor.DarkRed);
  }

  public override (string, ConsoleColor) Nearby()
  {
    return ("You smell the stench of an amarok nearby", ConsoleColor.Gray);
  }
}

public enum Hazard
{
  Pit,
  Maelstrom,
  Amarok,
}
