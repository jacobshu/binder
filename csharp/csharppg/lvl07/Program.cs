void getTriangleArea() {
  Console.WriteLine("What is the base of the triangle?");
  float triangleBase = Convert.ToSingle(Console.ReadLine());
  Console.WriteLine("Good, good. Now what is the height?");
  float triangleHeight = Convert.ToSingle(Console.ReadLine());
  Console.WriteLine("Yes, yes. Let me think...");
  Thread.Sleep(1000);
  Console.WriteLine("The area of your triangle is " + triangleBase * triangleHeight / 2);
}

// What egg counts end in the duckbear getting more eggs?
// 1, 2, 3, 6, 7, and 11
void divvyTheEggs() {
  Console.WriteLine("How many eggs are there?");
  int eggs = Convert.ToInt16(Console.ReadLine());
  int eggsPerSister = eggs / 4;
  int remainder = eggs % 4;
  Console.WriteLine("There are " + eggsPerSister + " for each sister and " + remainder + " for the duckbear");
}

void getKingdomPoints() {
  Console.WriteLine("How many estates do you have?");
  int estates = Convert.ToInt16(Console.ReadLine());
  Console.WriteLine("How many duchies do you have?");
  int duchies = Convert.ToInt16(Console.ReadLine());
  Console.WriteLine("How many provinces do you have?");
  int provinces = Convert.ToInt16(Console.ReadLine());
 
  int pointsPerEstate = 1;
  int pointsPerDuchy = 3;
  int pointsPerProvince = 
    6;
  int kingdomPoints = estates * pointsPerEstate + duchies * pointsPerDuchy + provinces * pointsPerProvince;
  Console.WriteLine("Your kingdom is worth " + kingdomPoints + " points.");
}

getKingdomPoints();
