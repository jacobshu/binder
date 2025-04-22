public class IsUniqueTest
{
  [Theory]
  [InlineData("abcde", true)]
  [InlineData("with space", true)]
  [InlineData("symbol!@#$%^&(", true)]
  [InlineData("ada", false)]
  [InlineData("definistrate", false)]
  [InlineData("symbol!@###$%^&(&", false)]
  public void IsUnique(string value, bool expected)
  {
    Assert.Equal(Chapter01.IsUnique(value), expected);
  }
}

public class IsPermutationTest
{
  [Theory]
  [InlineData("abc", "cba", true)]
  [InlineData("aaad", "aada", true)]
  [InlineData("da be", " beda", true)]
  [InlineData("ada", "aadd", false)]
  [InlineData("definistrate", "", false)]
  [InlineData("ze", "cz", false)]
  public void IsPermutation(string first, string second, bool expected)
  {
    Assert.Equal(Chapter01.IsPermutation(first, second), expected);
  }
}

public class UrlifyTest
{
  [Theory]
  [InlineData("ab c", 4, "ab%20c")]
  [InlineData(" qp98 ru1-  5713l", 16, "qp98%20ru1-%20%205713l")]
  [InlineData("", 0, "")]
  [InlineData(" 2 3 ", 3, "2%203")]
  public void Urlify(string url, int length, string expected)
  {
    Assert.Equal(Chapter01.Urlify(url, length), expected);
  }
}


