namespace Day4;

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

  public static uint Solve2(IEnumerable<string> lines)
  {
    var listOfMatchingNumbersCounts = new List<uint>();

    foreach (var line in lines)
    {
      var card = ParseLine(line);
      listOfMatchingNumbersCounts.Add(card.CountOfMatchingNumbers);
    }

    return calculateNumberOfCards(listOfMatchingNumbersCounts);
  }

  private static uint calculateNumberOfCards(List<uint> listOfMatchingNumbersCounts)
  {
    uint totalNumberOfCards = 0u;
    for (uint cardId = 1; cardId <= listOfMatchingNumbersCounts.Count; ++cardId)
    {
      totalNumberOfCards += calculateTotalCardCount(cardId, listOfMatchingNumbersCounts);
    }

    return totalNumberOfCards;
  }

  private static Dictionary<uint, uint> CARD_ID_TO_CARD_COUNT_CACHE = [];

  private static uint calculateTotalCardCount(uint cardId, List<uint> listOfMatchingNumbersCounts)
  {
    if (CARD_ID_TO_CARD_COUNT_CACHE.TryGetValue(cardId, out var totalCardCount))
    {
      return totalCardCount;
    }

    uint totalCountOfCards = 1;
    var numberOfExtraCards = cardId + listOfMatchingNumbersCounts[(int)(cardId - 1u)];
    for (uint extraCard = cardId + 1u; extraCard <= numberOfExtraCards; ++extraCard)
    {
      totalCountOfCards += calculateTotalCardCount(extraCard, listOfMatchingNumbersCounts);
    }

    return CARD_ID_TO_CARD_COUNT_CACHE[cardId] = totalCountOfCards;
  }

  private static (uint CountOfMatchingNumbers, uint Points) ParseLine(ReadOnlySpan<char> line)
  {
    int i = 0;
    var winningNumbers = new HashSet<int>();
    var countOfMatchingNumbers = 0u;
    Span<char> numberDigits = stackalloc char[10];

    for (; i < line.Length && !TryParseNextNumber(ref i, line, in numberDigits, out _); ++i)
    { }

    for (; i < line.Length && line[i] != '|'; ++i)
    {
      if (TryParseNextNumber(ref i, line, in numberDigits, out int number))
      {
        winningNumbers.Add(number);
      }
    }

    uint points = 0u;
    for (; i < line.Length; ++i)
    {
      if (TryParseNextNumber(ref i, line, in numberDigits, out int number) && winningNumbers.Contains(number))
      {
        ++countOfMatchingNumbers;
        points = points == 0u
          ? 1u
          : points << 1;
      }
    }

    return (countOfMatchingNumbers, points);
  }

  private static bool TryParseNextNumber(ref int index, ReadOnlySpan<char> line, in Span<char> numberDigits, out int number)
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
