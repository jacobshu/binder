int pilotNumber = AskForNumberInRange("Pick a number, Pilot", 0, 100);

Console.Clear();

int hunterNumber = -1;

Console.Write("What's the number, hunter? ");
do
{
  hunterNumber = Convert.ToInt16(Console.ReadLine());
  if (hunterNumber == pilotNumber) break;
  string qualifier = hunterNumber > pilotNumber ? "high" : "low";
  Console.WriteLine($"{hunterNumber} is too {qualifier}.");
  Console.WriteLine("What's your next guess?");
}
while (pilotNumber != hunterNumber);

Console.WriteLine("You guessed the number! Now fire away!");

string blastType = "";
for (int i = 0; i < 100; i++) {
  if (i % 3 == 0 && i % 5 == 0) {
    Console.ForegroundColor = ConsoleColor.Blue;
    blastType = "Hyper!";
  } else if (i%3 == 0) {
    Console.ForegroundColor = ConsoleColor.Red;
    blastType = "Fire";
  } else if (i%5 == 0) {
    Console.ForegroundColor = ConsoleColor.Yellow;
    blastType = "Electric";
  } else {
    Console.ForegroundColor = ConsoleColor.DarkGray;
    blastType = "Normal";
  }
  Console.WriteLine($"{i}: {blastType}");
}

int AskForNumberInRange(string text, int min, int max) 
{
  int result;
  do {
    Console.Write(text + " ");
    result = Convert.ToInt32(Console.ReadLine());
  }
  while (result < min || result > max);

  return result;
}
