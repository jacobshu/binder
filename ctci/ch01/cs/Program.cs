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

  public static string Urlify(string url, int length)
  {
    if (length == 0) return "";

    string result = "";
    bool startCounting = false;
    int count = 0;
    for (int i = 0; i < url.Length; i++)
    {
      char c = url[i];
      if (c == ' ')
      {
        if (!startCounting) continue;
        if (count >= length) break;
        result += "%20";
        count += 1;
      }
      else
      {
        startCounting = true;
        count += 1;
        result += c;
      }
    }

    return result;
  }
}
