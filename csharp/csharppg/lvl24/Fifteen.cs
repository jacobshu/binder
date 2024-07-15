
class Puzzle
{
  private string[][] state;

  static Puzzle()
  {
    Random rand = new Random();
    string[] source = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
    string[] target = new string[16];
    // fill target with x's to aid Find operations
    for (int i = 0; i < target.Length; i++)
    {
      target[i] = "x";
    }


    while (Array.FindAll(target, s => s == "x").Length > 2)
    {
      int i = rand.Next(16);
      if (target[i] != "x")
      {
        for (int j = 0; j < source.Length; j++)
        {
          if (source[j] != "*")
          {
            target[i] = source[j];
            source[j] = "*";
            break;
          }
        }
      }
    }
    for (int k = 0; k < 4; k++) {
      for (int l = 0; l < 4; l++) {
      state[]
    }
  }

  public void Print()
  {
    Console.WriteLine($"{state}");
  }
}
