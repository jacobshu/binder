Console.WriteLine("Make an arrow: ");
Console.Write("What for the head? ");
string? head = Console.ReadLine();
Console.Write("What for the fletching? ");
string? fletching = Console.ReadLine();
Console.Write("How long? ");
float length = Convert.ToSingle(Console.ReadLine());

HeadMaterial headParsed =
(head == "steel" || head.Contains("steel", StringComparison.CurrentCultureIgnoreCase)) ? HeadMaterial.Steel :
(head == "wood" || head.Contains("wood", StringComparison.CurrentCultureIgnoreCase)) ? HeadMaterial.Wood : HeadMaterial.Obsidian;

Fletching fletchingParsed =
(fletching.Contains("goose", StringComparison.CurrentCultureIgnoreCase)) ? Fletching.GooseFeather :
(fletching.Contains("turkey", StringComparison.CurrentCultureIgnoreCase)) ? Fletching.TurkeyFeather : Fletching.Plastic;

Arrow arrow = new Arrow(headParsed, fletchingParsed, length);
Console.WriteLine($"The arrow will cost: {arrow.GetCost()}");

class Arrow
{
  private HeadMaterial _head;
  private Fletching _fletching;
  private float _length;

  public Arrow(HeadMaterial head, Fletching fletching, float length)
  {
    if (length < 60 || length > 100)
    {
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.WriteLine("Cannot make an arrow that size.");
      Console.ResetColor();
      return;
    }

    _head = head;
    _fletching = fletching;
    _length = length;
  }

  public HeadMaterial GetHeadMaterial() => _head;
  public Fletching GetFletching() => _fletching;
  public float GetLength => _length;

  public float GetCost()
  {
    float headCost = _head == HeadMaterial.Steel ? 10 : _head == HeadMaterial.Wood ? 3 : 5;
    float fletchCost = _fletching == Fletching.GooseFeather ? 3 : _fletching == Fletching.TurkeyFeather ? 5 : 10;
    return _length * 0.5f + headCost + fletchCost;
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

