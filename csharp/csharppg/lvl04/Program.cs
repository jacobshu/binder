/* The Thing Namer 3000 repaired */
Console.WriteLine("What kind of thing are we talking about?");
// the thing itself
string a = Console.ReadLine();
Console.WriteLine("How would you describe it? Big? Azure? Tattered?");
// descriptor of the thing
string b = Console.ReadLine();
// hilarious adjectival phrase
string c = "of Doom";
// version number of thing
string d = "3000";
Console.WriteLine("The " + b + " " + a + " of " + c + " " + d + "!");

// What else could make this code more understandable?
// Name the variables semantically, according to their use
