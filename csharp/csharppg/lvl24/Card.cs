/*• Create a main method that will create a card instance for the whole deck (every color with every*/
/*rank) and display each (for example, “The Red Ampersand” and “The Blue Seven”).*/
/* Why use a color enumeration here but a color class previously? */
/* the color enumeration more naturally fits the situation for the Card class, 
 * it's unnecessary to be able to represent all colors in the RGB space */

enum CardColor
{
  Red,
  Green,
  Blue,
  Yellow,
}

enum CardRank
{
  One,
  Two,
  Three,
  Four,
  Five,
  Six,
  Seven,
  Eight,
  Nine,
  Ten,
  Dollar,
  Percent,
  Caret,
  Ampersand,
}

class Card
{
  public CardColor Color { get; set; }
  public CardRank Rank { get; set; }

  public Card(CardColor color, CardRank rank) {
    Color = color;
    Rank = rank;
  }

  public bool IsSymbolCard() => Rank == CardRank.Ampersand ||
    Rank == CardRank.Caret || Rank == CardRank.Dollar ||
    Rank == CardRank.Percent;

   
}

/* allowance for remote office */
/* allowance for equipment */
/* not to be disruptive, minumum hour overlap with team */
/* non-development responsibilities */
/* */

