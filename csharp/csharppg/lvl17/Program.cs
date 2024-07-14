(Food, Ingredient, Seasoning) makeMeal()
{
  Console.Write("Which type of food? ");
  string? foodType = Console.ReadLine();
  Console.Write("Which ingredient? ");
  string? ingredient = Console.ReadLine();
  Console.Write("Which seasoning? ");
  string? seasoning = Console.ReadLine();

  Food f;
  Ingredient i;
  Seasoning s;
  if (foodType.Contains("soup", StringComparison.CurrentCultureIgnoreCase))
  {
    f = Food.Soup;
  }
  else if (foodType.Contains("stew", StringComparison.CurrentCultureIgnoreCase))
  {
    f = Food.Stew;
  }
  else
  {
    f = Food.Gumbo;
  }

  if (ingredient.Contains("mushroom", StringComparison.CurrentCultureIgnoreCase))
  {
    i = Ingredient.Mushrooms;
  }
  else if (ingredient.Contains("chick", StringComparison.CurrentCultureIgnoreCase))
  {
    i = Ingredient.Chicken;
  }
  else if (ingredient.Contains("carrot", StringComparison.CurrentCultureIgnoreCase))
  {
    i = Ingredient.Carrots;
  }
  else
  {
    i = Ingredient.Potatoes;
  }

  if (seasoning.Contains("sweet", StringComparison.CurrentCultureIgnoreCase))
  {
    s = Seasoning.Sweet;
  }
  else if (seasoning.Contains("spic", StringComparison.CurrentCultureIgnoreCase))
  {
    s = Seasoning.Spicy;
  }
  else
  {
    s = Seasoning.Salty;
  }

  return (f, i, s);
};


(Food food, Ingredient ingredient, Seasoning seasoning) meal = makeMeal();
Console.WriteLine($"{meal.seasoning} {meal.ingredient} {meal.food}");

enum Food
{
  Soup,
  Stew,
  Gumbo,
}

enum Ingredient
{
  Mushrooms,
  Chicken,
  Carrots,
  Potatoes,
}

enum Seasoning
{
  Spicy,
  Salty,
  Sweet,
}
