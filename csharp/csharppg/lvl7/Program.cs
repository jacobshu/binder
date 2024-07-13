Console.WriteLine("What is the base of the triangle?");
float triangleBase = Convert.ToSingle(Console.ReadLine());
Console.WriteLine("Good, good. Now what is the height?");
float triangleHeight = Convert.ToSingle(Console.ReadLine());
Console.WriteLine("Yes, yes. Let me think...");
Thread.Sleep(1000);
Console.WriteLine("The area of your triangle is " + triangleBase * triangleHeight / 2);
