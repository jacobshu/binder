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
  public void IsUnique(string first, string second, bool expected)
  {
    Assert.Equal(Chapter01.IsPermutation(first, second), expected);
  }
}

