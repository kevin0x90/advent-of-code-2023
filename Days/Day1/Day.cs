namespace Days.Day1;

public class Day
{

  delegate bool GetDigitDelegate(ReadOnlySpan<char> input, int sliceStart, int sliceLength, out int digit);

  public static void Main(string[] args)
  {
    var input = ReadInput(@"Day1\Input.txt");
    var result1 = Solve1(input);
    var result2 = Solve2(input);

    Console.WriteLine(result1);
    Console.WriteLine(result2);
  }

  public static int Solve1(IEnumerable<string> lines)
    => lines
      .Sum(line => GetFirstAndLastDigitNumberFromLine(line, TryGetDigit1));

  public static int Solve2(IEnumerable<string> lines)
      => lines
        .Sum(line => GetFirstAndLastDigitNumberFromLine(line, TryGetDigit2));


  private static IEnumerable<string> ReadInput(string filename)
    => File.ReadLines(filename);

  private static int GetFirstAndLastDigitNumberFromLine(ReadOnlySpan<char> line, GetDigitDelegate TryGetDigitDelegate)
    => FindFirstDigit(ReverseLine(line), TryGetDigitDelegate) + (10 * FindFirstDigit(line, TryGetDigitDelegate));

  private static int FindFirstDigit(ReadOnlySpan<char> line, GetDigitDelegate TryGetDigitDelegate)
  {
    for (int i = 0; i < line.Length; ++i)
    {
      int remainingChars = line.Length - i;

      if (TryGetDigitDelegate(line, i, remainingChars, out int firstDigit))
      {
        return firstDigit;
      }
    }

    throw new InvalidDataException();
  }

  private static ReadOnlySpan<char> ReverseLine(ReadOnlySpan<char> line)
  {
    var reversedLine = new Span<char>(line.ToArray());
    reversedLine.Reverse();

    return reversedLine;
  }

  private static bool TryGetDigit1(ReadOnlySpan<char> line, int sliceStart, int sliceLength, out int digit)
  {
    digit = line.Slice(sliceStart, sliceLength) switch
    {
      "1"
      or ['1', ..]
      => 1,

      "2"
      or ['2', ..]
      => 2,

      "3"
      or ['3', ..]
      => 3,

      "4"
      or ['4', ..]
      => 4,

      "5"
      or ['5', ..]
      => 5,

      "6"
      or ['6', ..]
      => 6,

      "7"
      or ['7', ..]
      => 7,

      "8"
      or ['8', ..]
      => 8,

      "9"
      or ['9', ..]
      => 9,

      _ => -1
    };

    return digit != -1;
  }

  private static bool TryGetDigit2(ReadOnlySpan<char> line, int sliceStart, int sliceLength, out int digit)
  {
    digit = line.Slice(sliceStart, sliceLength) switch
    {
      "1"
      or ['1', ..]
      or ['o', 'n', 'e', ..]
      or ['e', 'n', 'o', ..]
      => 1,

      "2"
      or ['2', ..]
      or ['t', 'w', 'o', ..]
      or ['o', 'w', 't', ..]
      => 2,

      "3"
      or ['3', ..]
      or ['t', 'h', 'r', 'e', 'e', ..]
      or ['e', 'e', 'r', 'h', 't', ..]
      => 3,

      "4"
      or ['4', ..]
      or ['f', 'o', 'u', 'r', ..]
      or ['r', 'u', 'o', 'f', ..]
      => 4,

      "5"
      or ['5', ..]
      or ['f', 'i', 'v', 'e', ..]
      or ['e', 'v', 'i', 'f', ..]
      => 5,

      "6"
      or ['6', ..]
      or ['s', 'i', 'x', ..]
      or ['x', 'i', 's', ..]
      => 6,

      "7"
      or ['7', ..]
      or ['s', 'e', 'v', 'e', 'n', ..]
      or ['n', 'e', 'v', 'e', 's', ..]
      => 7,

      "8"
      or ['8', ..]
      or ['e', 'i', 'g', 'h', 't', ..]
      or ['t', 'h', 'g', 'i', 'e', ..]
      => 8,

      "9"
      or ['9', ..]
      or ['n', 'i', 'n', 'e', ..]
      or ['e', 'n', 'i', 'n', ..]
      => 9,

      _ => -1
    };

    return digit != -1;
  }
}
