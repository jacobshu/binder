public class Chapter01
{
  public static bool IsUnique(string str)
  {
    Dictionary<char, bool> found = new Dictionary<char, bool>();
    for (int i = 0; i < str.Length; i++)
    {
      if (found.ContainsKey(str[i]))
      {
        return false;
      }
      found.Add(str[i], true);
    }
    return true;
  }

  public static bool IsPermutation(string first, string second)
  {
    if (first.Length != second.Length) return false;

    Dictionary<char, int> charMap = new Dictionary<char, int>();
    for (int i = 0; i < first.Length; i++)
    {
      char firstChar = first[i];
      char secondChar = second[i];
      if (charMap.ContainsKey(firstChar))
      {
        charMap[firstChar] += 1;
      }
      else
      {
        charMap[firstChar] = 1;
      }

      if (charMap.ContainsKey(secondChar))
      {
        charMap[secondChar] -= 1;
      }
      else
      {
        charMap[secondChar] = -1;
      }
    }

    foreach (var pair in charMap)
    {
      if (pair.Value != 0) return false;
    }
    return true;
  }
}
