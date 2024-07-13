void duplicateArray()
{
  Console.WriteLine("Replicator online...");
  int[] array = new int[5];
  Console.Write("Provide five numbers. ");

  for (int i = 0; i < array.Length; i++)
  {
    string? input = Console.ReadLine();
    int num;
    if (int.TryParse(input, out num))
    {
      array[i] = num;
    }
    Console.Write("... ");
  }

  int[] dupArray = new int[5];
  Console.WriteLine("Duplicating...");
  Thread.Sleep(750);
  for (int i = 0; i < dupArray.Length; i++)
  {
    dupArray[i] = array[i];
  }

  Console.WriteLine("Verifying arrays...");
  Thread.Sleep(750);
  for (int i = 0; i < array.Length; i++)
  {
    Console.WriteLine($"{array[i]} : {dupArray[i]}");
  }
}

void minimum(int[] array)
{
  int currentSmallest = int.MaxValue;
  foreach (int number in array)
  {
    if (number < currentSmallest)
      currentSmallest = number;
  }
  Console.WriteLine(currentSmallest);
}

void average(int[] array)
{
  int total = 0;
  foreach (int number in array)
    total += number;
  float average = (float)total / array.Length;
  Console.WriteLine(average);
}
