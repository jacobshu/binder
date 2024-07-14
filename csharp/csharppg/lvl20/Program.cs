Console.WriteLine("Make an arrow: ");
Console.Write("What for the head? ");
string? head = Console.ReadLine();
Console.Write("What for the fletching? ");
string? fletching = Console.ReadLine();
Console.Write("How long? ");
float length = Convert.ToSingle(Console.ReadLine());

HeadMaterial headParsed =
(head == "steel" || head.Contains("stee", StringComparison.CurrentCultureIgnoreCase)) ? HeadMaterial.Steel :
(head == "wood" || head.Contains("woo", StringComparison.CurrentCultureIgnoreCase)) ? HeadMaterial.Wood : HeadMaterial.Obsidian;

Fletching fletchingParsed =
(fletching.Contains("goo", StringComparison.CurrentCultureIgnoreCase)) ? Fletching.GooseFeather :
(fletching.Contains("turk", StringComparison.CurrentCultureIgnoreCase)) ? Fletching.TurkeyFeather : Fletching.Plastic;

Arrow arrow = new Arrow(headParsed, fletchingParsed, length);
Console.WriteLine($"The arrow will cost: {arrow.GetCost()}");

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
  }

  public float GetCost()
  {
    float headCost = Head == HeadMaterial.Steel ? 10 : Head == HeadMaterial.Wood ? 3 : 5;
    float fletchCost = Fletch == Fletching.GooseFeather ? 3 : Fletch == Fletching.TurkeyFeather ? 5 : 10;
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

