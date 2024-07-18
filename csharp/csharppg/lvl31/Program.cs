public class Game
{

  public Game()
  {
  }
}

public class Board
{
  private int BoardSize { get; }
  private Point CurrentCursor { get; }
  private IRoom[,] BoardState;
  /*private Point MaelstromLocation { get; }*/
  /*private Point[] PitLocations { get; }*/
  /*private Point[] AmarokLocations { get; }*/


  public Board(int size)
  {
    CurrentCursor = new Point(0, 0);
    IRoom[,] newState = new IRoom[size, size];
    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size; j++)
      {
        newState[i, j] = new EmptyRoom(i, j);
      }
    }
    BoardState = newState;
  }

  public void Render()
  {
    int iterator = 0;
    for (int i = BoardState.GetLength(0) - 1; i >= 0; i--)
    {
      for (int j = 0; j < BoardState.GetLength(1); j++)
      {
        if (CurrentCursor.X == i && CurrentCursor.Y == j)
        {
          Console.BackgroundColor = ConsoleColor.Cyan;
        }
        Console.Write(BoardState[i, j] + " ");// + iterator + "\t");
        Console.ResetColor();
        iterator++;
      }
      Console.WriteLine();
    }
    Console.WriteLine($"Arrow Keys to move cursor. Press [Enter] to move a piece.");
    Console.WriteLine($"Press [R] to reset the board.");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.ResetColor();
  }
}

public interface IRoom
{
  public Point Location { get; }
  public bool IsEmpty { get; }

  public abstract void Enter();
  public abstract void Nearby();
}

public class EmptyRoom : IRoom
{
  public Point Location { get; }
  public bool IsEmpty { get; }

  public EmptyRoom(int x, int y)
  {
    Location = new Point(x, y);
    IsEmpty = true;
  }

  public void Enter()
  {

  }

  public void Nearby() { }
}

public record Point(int X, int Y);
