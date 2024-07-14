int manticoreHP = 10;
int cityHP = 15;

Console.WriteLine("Player 2, it is your turn.");
Console.WriteLine("-----------------------------------------------------------");

int manticoreNumber = AskForNumberInRange("How far away is the manticore? ", 0, 100);
Console.Clear();
loop(manticoreNumber);

int AskForNumberInRange(string text, int min, int max)
{
  int result;
  do
  {
    Console.Write(text + " ");
    result = Convert.ToInt32(Console.ReadLine());
  }
  while (result < min || result > max);

  return result;
}

void loop(int manticoreRange)
{
  bool endGame = false;
  int turn = 1;
  do
  {
    int damage = getDamage(turn);
    Console.WriteLine($"STATUS: Round: {turn} City: {cityHP}/15 Manticore: {manticoreHP}/10");
    Console.Write($"The cannon is expected to deal ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write($"{damage} damage");
    Console.ResetColor();
    Console.Write(" this round.\n");
    int cannonRange = AskForNumberInRange("Enter a desired cannon range", 0, 100);
    string qualifier;
    if (cannonRange == manticoreRange)
    {
      qualifier = "was a DIRECT HIT!";
      manticoreHP -= damage;
    }
    else if (cannonRange > manticoreRange)
    {
      qualifier = "OVERSHOT the target.";
    }
    else
    {
      qualifier = "FELL SHORT of the target.";
    };

    Console.WriteLine($"That round {qualifier}");
    Console.WriteLine("-----------------------------------------------------------");

    cityHP--;

    if (cityHP <= 0)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("The city of Consolas has fallen!");
      endGame = true;
    }

    if (manticoreHP <= 0)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("The Manticore has been destroyed, the city of Consolas is saved!");
      endGame = true;
    }
    turn++;
  }
  while (!endGame);

}

int getDamage(int round)
{
  int damage = 0;
  if (round % 3 == 0 && round % 5 == 0)
  {
    damage = 10;
  }
  else if (round % 3 == 0)
  {
    damage = 3;
  }
  else if (round % 5 == 0)
  {
    damage = 5;
  }
  else
  {
    damage = 1;
  }
  return damage;
}
