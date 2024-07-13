void clock()
{
  Console.Write("What's the number? ");
  int num = Convert.ToInt16(Console.Read());
  if (num % 2 == 0)
  {
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Tick");
  }
  else
  {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Tock");
  }
}

void watchtower()
{
  Console.Write("What's the x? ");
  int x = Convert.ToInt16(Console.ReadLine());
  Console.Write("What's the y? ");
  int y = Convert.ToInt16(Console.ReadLine());

  string yDir = y < 0 ? "south" : y == 0 ? "" : "north";
  string xDir = x < 0 ? "west" : x == 0 ? "" : "east";
  string dir = yDir + xDir;

  string warning = "The enemy is ";
  if (dir == "")
  {
    Console.WriteLine($"{warning} here!");
  }
  else
  {
    Console.WriteLine($"{warning} to the {dir}");
  }
}

watchtower();
