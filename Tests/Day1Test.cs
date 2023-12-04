using Day1;
using FluentAssertions;

namespace Day1Test
{
  [TestClass]
  public class Day1Test
  {
    [TestMethod]
    public void TestSampleInput1()
    {
      var sampleInput = new string[] {
       "1abc2",
       "npqr3stu8vwx",
       "a1b2c3d4e5f",
        "treb7uchet"
      };

      var result = Day.Solve1(sampleInput);

      result.Should().Be(142);
    }

    [TestMethod]
    public void TestSampleInput2()
    {
      var sampleInput = new string[]
      {
        "two1nine",
        "eightwothree",
        "abcone2threexyz",
        "xtwone3four",
        "4nineeightseven2",
        "zoneight234",
        "7pqrstsixteen"
      };

      var result = Day.Solve2(sampleInput);

      result.Should().Be(281);
    }
  }
}