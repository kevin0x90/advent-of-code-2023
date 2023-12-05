using System.Text.RegularExpressions;

namespace Day2;

public partial class Day
{
  private static uint maxRed = 12u;
  private static uint maxGreen = 13u;
  private static uint maxBlue = 14u;

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
    => (uint)lines
      .Select(line => GameIdWithSetsRegex().Matches(line))
      .Select(CreateGamesFromMatches)
      .Select(IsGameSetPossible)
      .Where(result => result.IsPossible)
      .Select(result => result.GameId)
      .Sum(gameId => gameId);


  public static uint Solve2(IEnumerable<string> lines)
    => (uint)lines
      .Select(line => GameIdWithSetsRegex().Matches(line))
      .Select(CreateGamesFromMatches)
      .Select(GameSetMinimumCubesPower)
      .Sum(cubesPower => cubesPower);

  private static (uint GameId, bool IsPossible) IsGameSetPossible(IEnumerable<Game> games)
  {
    uint gameId = 0u;
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

  private static uint GameSetMinimumCubesPower(IEnumerable<Game> games)
  {
    uint maxRed = 0u;
    uint maxGreen = 0u;
    uint maxBlue = 0u;

    foreach (var game in games)
    {
      maxRed = Math.Max(maxRed, game.RedCount);
      maxGreen = Math.Max(maxGreen, game.GreenCount);
      maxBlue = Math.Max(maxBlue, game.BlueCount);
    }

    return maxRed * maxGreen * maxBlue;
  }

  private static IEnumerable<Game> CreateGamesFromMatches(MatchCollection matches)
  {
    foreach (Match match in matches)
    {
      uint gameId = match.Groups["GameId"].Captures.Select(c => uint.Parse(c.Value)).First();

      foreach (Match setEntryMatch in SetEntryNumbersRegex().Matches(match.Groups["Set"].Value))
      {
        yield return new Game(gameId,
          GetSetEntryBallGroupCount(setEntryMatch, "Red"),
          GetSetEntryBallGroupCount(setEntryMatch, "Blue"),
          GetSetEntryBallGroupCount(setEntryMatch, "Green")
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

  [GeneratedRegex(@"^Game\s(?<GameId>\d+):(?<Set>.+)")]
  private static partial Regex GameIdWithSetsRegex();

  [GeneratedRegex(@"(?<Set>(\s(((?<Blue>\d+)\sblue)|((?<Red>\d+)\sred)|((?<Green>\d+)\sgreen))[,]?)+)")]
  private static partial Regex SetEntryNumbersRegex();

}