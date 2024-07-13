// ask for number

int AskForNumber(string text)
{
  Console.Write(text + " ");
  int result = Convert.ToInt32(Console.ReadLine());
  return result;
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

void Countdown(int number) 
{
  Console.WriteLine(number);
  number -= 1;
  if (number < 1) {
    return;
  } else {
    Countdown(number);
  }
}

Countdown(10);
