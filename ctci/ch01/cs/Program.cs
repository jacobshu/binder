namespace Chapter01
{
  public class Problem1_1
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
  }
}
