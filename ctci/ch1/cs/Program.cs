bool IsUnique(string str)
{
  Dictionary<char, bool> found = new Dictionary<char, bool>();
  for (int i = 0; i < str.Length; i++)
  {
    if (found.ContainsKey(str[i]))
      return false;
  }
  return true;
}
