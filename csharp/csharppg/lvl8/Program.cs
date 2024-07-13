Console.Write("What's the target row? ");
int y = Convert.ToInt16(Console.ReadLine());
Console.Write("What's the target column? ");
int x = Convert.ToInt16(Console.ReadLine());

string top = $"({x}, {y + 1})";
string right = $"({x + 1}, {y})";
string bottom = $"({x}, {y - 1})";
string left = $"({x - 1}, {y})";

Console.WriteLine("Deploy squad to: ");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(top);
Console.WriteLine(right);
Console.WriteLine(bottom);
Console.WriteLine(left);
/*Console.Beep(440, 500);*/
