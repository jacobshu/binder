Console.Write("Want a premade arrow? We have beginner, marksman, and elite versions. ");
string isPremade = Console.ReadLine() ?? "n";

Arrow theArrow;
if (isPremade.Contains("y", StringComparison.CurrentCultureIgnoreCase))
{
  Console.Write("Which version would you like? ");
  string version = Console.ReadLine() ?? "beginner";
  if (version.Contains("begin", StringComparison.CurrentCultureIgnoreCase))
  {
    theArrow = Arrow.Beginner();
  }
  else if (version.Contains("mark", StringComparison.CurrentCultureIgnoreCase))
  {
    theArrow = Arrow.Marksman();
  }
  else
  {
    theArrow = Arrow.Elite();
  }
}
else
{
  Console.WriteLine("Then choose your materials. ");
  Console.Write("What for the head? ");
  string head = Console.ReadLine() ?? "wood";
  Console.Write("What for the fletching? ");
  string fletching = Console.ReadLine() ?? "goose";
  Console.Write("How long? ");
  float length = Convert.ToSingle(Console.ReadLine());

  HeadMaterial headParsed =
  (head == "steel" || head.Contains("stee", StringComparison.CurrentCultureIgnoreCase)) ? HeadMaterial.Steel :
  (head == "wood" || head.Contains("woo", StringComparison.CurrentCultureIgnoreCase)) ? HeadMaterial.Wood : HeadMaterial.Obsidian;

  Fletching fletchingParsed =
  (fletching.Contains("goo", StringComparison.CurrentCultureIgnoreCase)) ? Fletching.GooseFeather :
  (fletching.Contains("turk", StringComparison.CurrentCultureIgnoreCase)) ? Fletching.TurkeyFeather : Fletching.Plastic;

  theArrow = new Arrow(headParsed, fletchingParsed, length);
}

Console.WriteLine($"The arrow will cost: {theArrow.GetCost()}");

class Arrow
{
  public HeadMaterial Head { get; set; }
  public Fletching Fletch { get; set; }
  public float Length { get; }

  public Arrow(HeadMaterial head, Fletching fletching, float length)
  {
    if (length < 60 || length > 100)
    {
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.WriteLine("Cannot make an arrow that size.");
      Console.ResetColor();
      return;
    }

    Head = head;
    Fletch = fletching;
    Length = length;
  }

  public static Arrow Beginner() => new Arrow(HeadMaterial.Wood, Fletching.GooseFeather, 75);
  public static Arrow Marksman() => new Arrow(HeadMaterial.Steel, Fletching.GooseFeather, 65);
  public static Arrow Elite() => new Arrow(HeadMaterial.Steel, Fletching.Plastic, 95);

  public float GetCost()
  {
    Console.WriteLine($"The arrow has a {Head} head, {Fletch} fletching, and is {Length}cm long.");
    float headCost;
    switch (Head) {
      case HeadMaterial.Steel:
        headCost = 10;
        break;
      case HeadMaterial.Obsidian:
        headCost = 5;
        break;
      default:
        headCost = 3;
        break;
    }

    float fletchCost;
    switch (Fletch) {
      case Fletching.Plastic:
        fletchCost = 10;
        break;
      case Fletching.TurkeyFeather:
        fletchCost = 5;
        break;
      default: 
        fletchCost = 3;
        break;
    }
    return Length * 0.5f + headCost + fletchCost;
  }
}

enum HeadMaterial
{
  Steel,
  Wood,
  Obsidian,
}

enum Fletching
{
  Plastic,
  GooseFeather,
  TurkeyFeather,
}

