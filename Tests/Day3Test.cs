using Day3;
using FluentAssertions;

namespace Day3Test
{
  [TestClass]
  public class Day3Test
  {
    [TestMethod]
    public void TestSampleInput1()
    {
      var sampleInput = new string[] {
        "467..114..",
        "...*......",
        "..35..633.",
        "......#...",
        "617*......",
        ".....+.58.",
        "..592.....",
        "......755.",
        "...$.*....",
        ".664.598.."
      };

      var result = Day.Solve1(sampleInput);

      result.Should().Be(4361);
    }

    [TestMethod]
    public void TestSampleInput2()
    {
      var sampleInput = new string[] {
        "467..114..",
        "...*......",
        "..35..633.",
        "......#...",
        "617*......",
        ".....+.58.",
        "..592.....",
        "......755.",
        "...$.*....",
        ".664.598.."
      };

      var result = Day.Solve2(sampleInput);

      result.Should().Be(467835);
    }
  }
}