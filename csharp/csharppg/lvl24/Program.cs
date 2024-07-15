Point p = new Point(2, 3);
Point q = new Point(-4, 0);
Console.WriteLine($"({p.X}, {p.Y})");
Console.WriteLine($"({q.X}, {q.Y})");

Color a = Color.Red;
Color b = new Color(64, 128, 255);
Console.WriteLine($"{a.R}, {a.G}, {a.B}");
Console.WriteLine($"{b.R}, {b.G}, {b.B}");

CardRank[] ranks = new CardRank[]{
  CardRank.One,
  CardRank.Two,
  CardRank.Three,
  CardRank.Four,
  CardRank.Five,
  CardRank.Six,
  CardRank.Seven,
  CardRank.Eight,
  CardRank.Nine,
  CardRank.Ten,
  CardRank.Dollar,
  CardRank.Percent,
  CardRank.Caret,
  CardRank.Ampersand,
};

CardColor[] colors = new CardColor[]{
  CardColor.Red,
  CardColor.Green,
  CardColor.Blue,
  CardColor.Yellow,
};

foreach (CardColor color in colors)
{
  foreach (CardRank rank in ranks)
  {
    Card card = new Card(color, rank);
    Console.WriteLine($"The {card.Color} {card.Rank}");
  }
}

/*while (true)*/
/*{*/
/*  Console.Write("Password, please. ");*/
/*  string password = Console.ReadLine() ?? "";*/
/*  bool isValid = PasswordValidator.Validate(password);*/
/*  string validStr = isValid ? "is" : "is not";*/
/*  Console.WriteLine($"password {validStr} valid");*/
/*}*/

Puzzle z = new Puzzle();
z.Print();
