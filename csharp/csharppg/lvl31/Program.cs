Console.ForegroundColor = ConsoleColor.Gray;
Console.Write("How big are the caverns? ");
int size = Convert.ToUInt16(Console.ReadLine());
Console.Clear();
Game game = new Game(size);
game.Run();

public record Point(int X, int Y);

class PointEqualityComparer : IEqualityComparer<Point>
{
  public bool Equals(Point? p1, Point? p2)
  {
    if (ReferenceEquals(p1, p2))
      return true;

    if (p2 is null || p1 is null)
      return false;

    return p1.X == p2.X
        && p1.Y == p2.Y;
  }

  public int GetHashCode(Point point) => point.X ^ point.Y;
}
