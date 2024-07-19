Console.ForegroundColor = ConsoleColor.Gray;
Console.Write("How big are the caverns? ");
int size = Convert.ToUInt16(Console.ReadLine());
Console.Clear();
Game game = new Game(size);
game.Run();

public record Point(int X, int Y);
