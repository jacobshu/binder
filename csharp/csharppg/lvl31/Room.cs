public abstract class Room
{
  public Point Location { get; }
  public Hazard? RoomHazard { get; }
  private ConsoleColor Color { get; }

  public Room(int x, int y, Hazard? hazard)
  {
    Location = new Point(x, y);
    RoomHazard = hazard;
  }

  public abstract void Render();

  public bool IsEmpty() => RoomHazard == null;

  public string Nearby()
  {
    return "";
  }
}

public class EmptyRoom : Room
{
  public EmptyRoom(int x, int y) : base(x, y, null) { }

  public override void Render()
  {
    Console.Write(" • ");
  }
}

public class PitRoom : Room
{
  public PitRoom(int x, int y) : base(x, y, Hazard.Pit) { }
  public override void Render()
  {
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write("O");
    Console.ResetColor();
  }
}

public class MaelstromRoom : Room
{
  public MaelstromRoom(int x, int y) : base(x, y, Hazard.Maelstrom) { }
  public override void Render()
  {
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("§");
    Console.ResetColor();
  }
}

public class AmarokRoom : Room
{
  public AmarokRoom(int x, int y) : base(x, y, Hazard.Amarok) { }
  public override void Render()
  {
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write("#");
    Console.ResetColor();
  }
}

public enum Hazard
{
  Pit,
  Maelstrom,
  Amarok,
}
