namespace Day4;

public static class Day
{
  public static void Main(string[] args)
  {
    var input = ReadInput("Input.txt");

    var result1 = Solve1(input);
    Console.WriteLine(result1);
  }

  private static IEnumerable<string> ReadInput(string filename)
        => File.ReadLines(filename);

  public static uint Solve1(IEnumerable<string> lines)
  {
    uint sum = 0u;
    foreach (var line in lines)
    {
      var card = ParseLine(line);
      sum += card.Points;
    }

    return sum;
  }

  private static (uint CardNumber, uint Points) ParseLine(ReadOnlySpan<char> line)
  {
    int i = 0;
    int cardNumber = 0;
    var winningNumbers = new HashSet<int>();
    Span<char> numberDigits = stackalloc char[10];

    for (; i < line.Length && !TryParseNextNumber(ref i, line, ref numberDigits, out cardNumber); ++i)
    { }

    for (; i < line.Length && line[i] != '|'; ++i)
    {
      if (TryParseNextNumber(ref i, line, ref numberDigits, out int number))
      {
        winningNumbers.Add(number);
      }
    }

    uint points = 0u;
    for (; i < line.Length; ++i)
    {
      if (TryParseNextNumber(ref i, line, ref numberDigits, out int number) && winningNumbers.Contains(number))
      {
        points = points == 0u
          ? 1u
          : points << 1;
      }
    }

    return ((uint)cardNumber, points);
  }

  private static bool TryParseNextNumber(ref int index, ReadOnlySpan<char> line, ref Span<char> numberDigits, out int number)
  {
    int digitIndex = 0;
    while (index < line.Length && char.IsDigit(line[index]))
    {
      numberDigits[digitIndex++] = line[index];
      ++index;
    }

    var isNumber = digitIndex > 0;
    if (isNumber)
    {
      number = int.Parse(numberDigits);
      numberDigits.Clear();
    }
    else
    {
      number = default;
    }

    return isNumber;
  }
}
