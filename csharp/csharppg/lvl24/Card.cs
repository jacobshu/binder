/*Each card has a color*/
/*(red, green, blue, yellow) and a rank (the numbers 1 through 10, followed by the symbols $, %, ^, and &).*/
/*The third pedestal requires that you create a class to represent a card of this nature.*/
/*Objectives:*/
/*• Define enumerations for card colors and card ranks.*/
/*• Define a Card class to represent a card with a color and a rank, as described above.*/
/*• Add properties or methods that tell you if a card is a number or symbol card (the equivalent of a*/
/*face card).*/
/*• Create a main method that will create a card instance for the whole deck (every color with every*/
/*rank) and display each (for example, “The Red Ampersand” and “The Blue Seven”).*/
/*• Answer this question: Why do you think we used a color enumeration here but made a color class*/
/*in the previous challenge?*/

enum CardColor
{
  Red,
  Green,
  Blue,
  Yellow,
}

enum CardRank
{
  _1,
  _2,
  _3,
  _4,
  _5,
  _6,
  _7,
  _8,
  _9,
  _10,
  _Dollar,
  _Percent,
  _Caret,
  _Ampersand,
}

class Card
{
  public CardColor Color { get; }
  public CardRank Rank { get; }

  public bool IsSymbolCard() => Rank == CardRank._Ampersand ||
    Rank == CardRank._Caret || Rank == CardRank._Dollar ||
    Rank == CardRank._Percent;


}
