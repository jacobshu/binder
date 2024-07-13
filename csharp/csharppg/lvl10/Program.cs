Console.Write("Hello there! What's your name? ");
string? name = Console.ReadLine();
name = name == "" ? "Shadow" : name;
Console.WriteLine("the name is " + name);
Console.WriteLine("The following items are available: ");
Console.WriteLine("1 – Rope");
Console.WriteLine("2 – Torches");
Console.WriteLine("3 – Climbing Equipment");
Console.WriteLine("4 – Clean Water");
Console.WriteLine("5 – Machete");
Console.WriteLine("6 – Canoe");
Console.WriteLine("7 – Food Supplies");
Console.Write("What number do you want to see the price of? ");
string? itemNumber = Console.ReadLine();

string itemName = "";
string costString = "s";
int itemCost = 0;
float costMultiplier = 1;
if (name == "Locke")
{
  costMultiplier = 0.5f;
}
else if (name == "Shadow")
{
  costMultiplier = 2.0f;
}

switch (itemNumber)
{
  case "1":
    itemCost = 10;
    itemName = "Rope";
    break;
  case "2":
    itemCost = 16;
    itemName = "Torches";
    costString = "";
    break;
  case "3":
    itemCost = 24;
    itemName = "Climbing equipment";
    break;
  case "4":
    itemCost = 2;
    itemName = "Clean water";
    break;
  case "5":
    itemCost = 20;
    itemName = "A machete";
    break;
  case "6":
    itemCost = 200;
    itemName = "A canoe";
    break;
  case "7":
    itemCost = 2;
    itemName = "Food supplies";
    costString = "";
    break;
}

if (itemCost == 0)
{
  Console.WriteLine("We don't have any of that");
}
else
{

  string address = costMultiplier == 1 ? "" : $"For you, {name}, ";
  Console.WriteLine($"{address}{itemName} cost{costString} {itemCost * costMultiplier}.");
}

