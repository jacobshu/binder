Robot r2 = new Robot();
r2.Init();
r2.Run();

public class Robot
{
  public int X { get; set; }
  public int Y { get; set; }
  public bool IsPowered { get; set; }
  public IRobotCommand?[] Commands { get; } = new IRobotCommand?[3];

  public void Init()
  {
    Console.WriteLine("Command the robot: ");
    Console.WriteLine("Toggle the robot's [p]ower. Go [n]orth, [s]outh, [e]ast, or [w]est.");
    Commands[0] = ParseInput(Console.ReadLine());
    Commands[1] = ParseInput(Console.ReadLine());
    Commands[2] = ParseInput(Console.ReadLine());
  }

  public void Run()
  {
    foreach (IRobotCommand? command in Commands)
    {
      command?.Run(this);
      Console.WriteLine($"[{X} {Y} {IsPowered}]");
    }
  }

  private IRobotCommand ParseInput(string input)
  {
    if (input == "p" || input.Contains("power", StringComparison.CurrentCultureIgnoreCase))
    {
        return new OnCommand();
    } else if (input == "n" || input.Contains("north", StringComparison.CurrentCultureIgnoreCase))
    {
      return new NorthCommand();
    } else if (input == "s" || input.Contains("south", StringComparison.CurrentCultureIgnoreCase))
    {
      return new SouthCommand();
    } else if (input == "e" || input.Contains("east", StringComparison.CurrentCultureIgnoreCase))
    {
      return new EastCommand();
    } else if (input == "w" || input.Contains("west", StringComparison.CurrentCultureIgnoreCase))
    {
      return new WestCommand();
    } else {
      return new OffCommand();
    }
  }
}

public interface IRobotCommand
{
  void Run(Robot robot);
}

public class OnCommand : IRobotCommand
{
  public void Run(Robot robot)
  { 
    robot.IsPowered = true;
  }
}

public class OffCommand : IRobotCommand
{
  public void Run(Robot robot)
  {
    robot.IsPowered = false;
  }
}

public class NorthCommand : IRobotCommand
{
  public void Run(Robot robot)
  {
    if (robot.IsPowered) robot.Y += 1;
  }
}

public class SouthCommand : IRobotCommand
{
  public void Run(Robot robot)
  {
    if (robot.IsPowered) robot.Y -= 1;
  }
}

public class EastCommand : IRobotCommand
{
  public void Run(Robot robot)
  {
    if (robot.IsPowered) robot.X += 1;
  }
}

public class WestCommand : IRobotCommand
{
  public void Run(Robot robot)
  {
    if (robot.IsPowered) robot.X -= 1;
  }
}
