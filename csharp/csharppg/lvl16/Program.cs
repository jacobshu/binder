ChestState state = ChestState.Closed;

while (true)
{
  Console.Write($"The chest is {state}. What would you like to do? ");
  string? input = Console.ReadLine();
  Action action = parseAction(input);
  state = update(action, state);
}

Action parseAction(string input)
{
  Action newAction = Action.Open;
  if (input == "o" || input == "O" || input.Contains("Open", StringComparison.CurrentCultureIgnoreCase))
  {
    newAction = Action.Open;
  } else if (input == "U" || input == "u" || input.Contains("unlock", StringComparison.CurrentCultureIgnoreCase))
  {
    newAction = Action.Unlock;
  } else if (input == "C" || input == "c" || input.Contains("close", StringComparison.CurrentCultureIgnoreCase))
  {
    newAction = Action.Close;
  } else {
    newAction = Action.Lock;
  }
  return newAction;
}

ChestState update(Action action, ChestState cs)
{
  ChestState newState = cs;
  switch (action)
  {
    case Action.Close:
      newState = (cs == ChestState.Closed || cs == ChestState.Open) ? ChestState.Closed : ChestState.Locked;
      break;
    case Action.Lock:
      newState = (cs == ChestState.Locked || cs == ChestState.Closed) ? ChestState.Locked : ChestState.Open;
      break;
    case Action.Open:
      newState = (cs == ChestState.Open || cs == ChestState.Closed) ? ChestState.Open : ChestState.Locked;
      break;
    case Action.Unlock:
      newState = (cs == ChestState.Closed || cs == ChestState.Locked) ? ChestState.Closed : ChestState.Open;
      break;
  }
  return newState;
}

enum ChestState
{
  Open,
  Closed,
  Locked,
}

enum Action
{
  Close,
  Lock,
  Open,
  Unlock,
}
