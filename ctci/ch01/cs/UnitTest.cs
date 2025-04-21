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
    Assert.Equal(Chapter01.Problem1_1.IsUnique(value), expected);
  }
}

