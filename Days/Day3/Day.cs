namespace Day3;

public static class Day
{
  public static void Main(string[] args)
  {
    var input = ReadInput("Input.txt");

    var result1 = Solve1(input);
    Console.WriteLine(result1);

    var result2 = Solve2(input);
    Console.WriteLine(result2);
  }

  private static IEnumerable<string> ReadInput(string filename)
        => File.ReadLines(filename);

  public static int Solve1(IEnumerable<string> lines)
  {
    ReadOnlySpan<char> previousLine = null;
    ReadOnlySpan<char> nextLine = null;

    int sum = 0;
    IEnumerator<string> enumerator = lines.GetEnumerator();
    while (enumerator.MoveNext())
    {
      ReadOnlySpan<char> currentLine = nextLine != null
        ? nextLine
        : enumerator.Current;

      nextLine = nextLine != null
        ? enumerator.Current
        : enumerator.GetNextLine();

      sum += GetSymbolAdjacentNumbersSum(previousLine, nextLine, currentLine);

      previousLine = currentLine;
    }

    return sum;
  }

  public static int Solve2(IEnumerable<string> lines)
  {
    ReadOnlySpan<char> previousLine = null;
    ReadOnlySpan<char> nextLine = null;

    int sum = 0;
    IEnumerator<string> enumerator = lines.GetEnumerator();
    while (enumerator.MoveNext())
    {
      ReadOnlySpan<char> currentLine = nextLine != null
        ? nextLine
        : enumerator.Current;

      nextLine = nextLine != null
        ? enumerator.Current
        : enumerator.GetNextLine();

      sum += GetGearAdjacentNumbersSum(previousLine, nextLine, currentLine);

      previousLine = currentLine;
    }

    return sum;
  }

  private static int GetSymbolAdjacentNumbersSum(
    ReadOnlySpan<char> previousLine, ReadOnlySpan<char> nextLine, ReadOnlySpan<char> currentLine)
  {
    int sum = 0;
    for (int i = 0; i < currentLine.Length; ++i)
    {
      if (!currentLine[i].IsSymbol())
      {
        continue;
      }

      sum += currentLine.GetAdjacentNumbers(i).Sum();

      if (previousLine != null)
      {
        sum += previousLine.GetAdjacentNumbers(i).Sum();
      }

      if (nextLine != null)
      {
        sum += nextLine.GetAdjacentNumbers(i).Sum();
      }
    }

    return sum;
  }

  private static int GetGearAdjacentNumbersSum(
      ReadOnlySpan<char> previousLine, ReadOnlySpan<char> nextLine, ReadOnlySpan<char> currentLine)
  {
    int sum = 0;
    for (int i = 0; i < currentLine.Length; ++i)
    {
      if (!currentLine[i].IsGear())
      {
        continue;
      }

      List<int> adjacentNumbers = currentLine.GetAdjacentNumbers(i);

      if (previousLine != null)
      {
        adjacentNumbers.AddRange(previousLine.GetAdjacentNumbers(i));
      }

      if (nextLine != null)
      {
        adjacentNumbers.AddRange(nextLine.GetAdjacentNumbers(i));
      }

      if (adjacentNumbers.Count == 2)
      {
        sum += adjacentNumbers[0] * adjacentNumbers[1];
      }
    }

    return sum;
  }

  private static List<int> GetAdjacentNumbers(this ReadOnlySpan<char> line, int currentPosition)
  {
    if (line.TryGetNumberFromPosition(currentPosition, out int directNumber))
    {
      return [directNumber];
    }

    var neighbourNumbers = new List<int>(2);

    if (line.TryGetNumberFromPosition(currentPosition - 1, out int leftNeighbourNumber))
    {
      neighbourNumbers.Add(leftNeighbourNumber);
    }

    if (line.TryGetNumberFromPosition(currentPosition + 1, out int rightNeighbourNumber))
    {
      neighbourNumbers.Add(rightNeighbourNumber);
    }

    return neighbourNumbers;
  }

  private static bool TryGetNumberFromPosition(this ReadOnlySpan<char> line, int positionToCheck, out int number)
  {
    if (positionToCheck >= 0 && positionToCheck < line.Length && char.IsDigit(line[positionToCheck]))
    {
      int start = positionToCheck;
      while (start - 1 >= 0 && char.IsDigit(line[start - 1])) { start -= 1; }

      int end = positionToCheck;
      while (end + 1 < line.Length && char.IsDigit(line[end + 1])) { end += 1; }

      number = int.Parse(line.Slice(start, end - start + 1));
      return true;
    }

    number = default;
    return false;
  }

  private static string? GetNextLine(this IEnumerator<string> enumerator)
  {
    return enumerator.MoveNext()
      ? enumerator.Current
      : null;
  }

  private static bool IsSymbol(this char character)
  {
    return character != '.' && !char.IsDigit(character);
  }

  private static bool IsGear(this char character)
  {
    return character == '*';
  }
}
