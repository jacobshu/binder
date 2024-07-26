public abstract class Room
{
  public Hazard? RoomHazard { get; }
  private ConsoleColor Color { get; }
  public Point RoomCursor { get; }
  public Game CurrentGame { get; }

  public Room(int x, int y, Hazard? hazard, Game game)
  {
    RoomHazard = hazard;
    RoomCursor = new Point(x, y);
    CurrentGame = game;
  }

  public abstract void Render(bool isCurrentCursor);

  public bool IsEmpty() => RoomHazard == null;

  public abstract Message Nearby();
  public abstract void Enter();
}

public class EmptyRoom : Room
{
  public EmptyRoom(int x, int y, Game g) : base(x, y, null, g) { }

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

  public override void Enter()
  {
  }

  public override Message Nearby()
  {
    return new Message("", ConsoleColor.Gray);
  }
}

public class FountainRoom : Room
{
  public FountainRoom(int x, int y, Game g) : base(x, y, null, g) { }
  public override void Render(bool isCurrentCursor)
  {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(" Θ ");
    Console.ResetColor();
  }

  public override void Enter()
  {
    CurrentGame.TriggerEndGame();
    CurrentGame.SetEndGameMessage(new Message("The fountain glitters before you!", ConsoleColor.Yellow));
  }

  public override Message Nearby()
  {
    return new Message("You hear water dripping nearby...", ConsoleColor.Yellow);
  }
}

public class PitRoom : Room
{
  public PitRoom(int x, int y, Game g) : base(x, y, Hazard.Pit, g) { }
  public override void Render(bool isCurrentCursor)
  {
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write(" O ");
    Console.ResetColor();
  }

  public override void Enter()
  {
    CurrentGame.TriggerEndGame();
    CurrentGame.SetEndGameMessage(new Message("You fall to your demise.", ConsoleColor.Black));
  }

  public override Message Nearby()
  {
    return new Message("You feel a draft coming from nearby.", ConsoleColor.Black);
  }
}

public class MaelstromRoom : Room
{
  public MaelstromRoom(int x, int y, Game g) : base(x, y, Hazard.Maelstrom, g) { }
  public override void Render(bool isCurrentCursor)
  {
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(" § ");
    Console.ResetColor();
  }

  public override void Enter()
  {
    CurrentGame.TriggerEndGame();
    CurrentGame.SetEndGameMessage(new Message("You are taken up in a maelstrom!", ConsoleColor.Blue));
  }

  public override Message Nearby()
  {
    return new Message("You hear the growling of a maelstrom nearby", ConsoleColor.Blue);
  }
}

public class AmarokRoom : Room
{
  public AmarokRoom(int x, int y, Game g) : base(x, y, Hazard.Amarok, g) { }
  public override void Render(bool isCurrentCursor)
  {
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write(" Ψ ");
    Console.ResetColor();
  }

  public override void Enter()
  {
    CurrentGame.TriggerEndGame();
    CurrentGame.SetEndGameMessage(new Message("You are torn asunder by an amarok!", ConsoleColor.DarkRed));
  }

  public override Message Nearby()
  {
    return new Message("You smell the stench of an amarok nearby", ConsoleColor.DarkRed);
  }
}

public enum Hazard
{
  Pit,
  Maelstrom,
  Amarok,
}
