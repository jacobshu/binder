public abstract class Room
{
  public Hazard? RoomHazard { get; }
  private ConsoleColor Color { get; }

  public Room(Hazard? hazard)
  {
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
  public EmptyRoom() : base(null) { }

  public override void Render()
  {
    Console.Write(" • ");
  }
}

public class PitRoom : Room
{
  public PitRoom() : base(Hazard.Pit) { }
  public override void Render()
  {
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write(" O ");
    Console.ResetColor();
  }
}

public class MaelstromRoom : Room
{
  public MaelstromRoom() : base(Hazard.Maelstrom) { }
  public override void Render()
  {
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(" § ");
    Console.ResetColor();
  }
}

public class AmarokRoom : Room
{
  public AmarokRoom() : base(Hazard.Amarok) { }
  public override void Render()
  {
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write(" # ");
    Console.ResetColor();
  }
}

public enum Hazard
{
  Pit,
  Maelstrom,
  Amarok,
}
