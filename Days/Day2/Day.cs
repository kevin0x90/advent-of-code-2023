using System.Text.RegularExpressions;

namespace Day2;

public partial class Day
{
  private static uint maxRed = 12;
  private static uint maxGreen = 13;
  private static uint maxBlue = 14;

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
    return (uint)lines
      .Select(line => GameIdWithSetsRegex().Matches(line))
      .Select(CreateGamesFromMatches)
      .Select(IsGameSetPossible)
      .Where(result => result.isPossible)
      .Select(result => result.GameId)
      .Sum(gameId => gameId);
  }

  private static (uint GameId, bool isPossible) IsGameSetPossible(IEnumerable<Game> games)
  {
    uint gameId = 0;
    foreach (var game in games)
    {
      gameId = game.Id;
      if (!(game.RedCount <= maxRed && game.BlueCount <= maxBlue && game.GreenCount <= maxGreen))
      {
        return (gameId, false);
      }
    }

    return (gameId, true);
  }

  private static IEnumerable<Game> CreateGamesFromMatches(MatchCollection matches)
  {
    foreach (Match match in matches)
    {
      uint gameId = match.Groups["GameId"].Captures.Select(c => uint.Parse(c.Value)).First();

      foreach (Match setEntryMatch in SetEntryNumbersRegex().Matches(match.Groups["set"].Value))
      {
        yield return new Game(gameId,
          GetSetEntryBallGroupCount(setEntryMatch, "red"),
          GetSetEntryBallGroupCount(setEntryMatch, "blue"),
          GetSetEntryBallGroupCount(setEntryMatch, "green")
        );
      }
    }
  }

  private static uint GetSetEntryBallGroupCount(Match setEntryMatch, string ballGroupName)
  {
    if (setEntryMatch.Groups.TryGetValue(ballGroupName, out var ballGroup))
    {
      return (uint)ballGroup.Captures
        .Select(c => uint.Parse(c.Value))
        .Sum(value => value);
    }

    return 0u;
  }

  [GeneratedRegex(@"^Game\s(?<GameId>\d+):(?<set>.+)")]
  private static partial Regex GameIdWithSetsRegex();

  [GeneratedRegex(@"(?<set>(\s(((?<blue>\d+)\sblue)|((?<red>\d+)\sred)|((?<green>\d+)\sgreen))[,]?)+)")]
  private static partial Regex SetEntryNumbersRegex();

}