using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Bet.Queries;

public class GetAllUserBetsByLeagueAndStatusQueryHandler(IBetRepository repository, ICurrentUserService userService)
    : IRequestHandler<GetAllUserBetsByLeagueAndStatusQuery, GetUserBetsResult>
{
    public async Task<GetUserBetsResult> Handle(GetAllUserBetsByLeagueAndStatusQuery request,
        CancellationToken cancellationToken)
    {
        var bets = await repository.GetAllByLeagueIdAndStatusAsync(userService.UserId, request.LeagueId,
            request.Status);

        var betDtos = bets.Select(b => new BetDto
        {
            Id = b.Id,
            Match = b.Match.ToDto(),
            League = b.League.ToDto(),
            PredictedHomeScore = b.PredictedHomeScore,
            PredictedAwayScore = b.PredictedAwayScore,
            PointsEarned = b.PointsEarned,
            PlacedAt = b.PlacedAt,
            IsProcessed = b.IsProcessed,
            PointsDetail = GetPointsDetail(b)
        }).ToList();

        var statistics = CalculateStatistics(bets);

        return new GetUserBetsResult
        {
            Bets = betDtos,
            Statistics = statistics
        };
    }

    private static BetStatistics CalculateStatistics(List<Domain.Entities.Bet> bets)
    {
        var finishedBets = bets.Where(b => b.Match.Status == MatchStatus.Finished).ToList();
        var betsWithPoints = finishedBets.Where(b => b.PointsEarned > 0).ToList();

        return new BetStatistics
        {
            TotalBets = bets.Count,
            PendingBets = bets.Count(b => b.Match.Status == MatchStatus.Scheduled),
            FinishedBets = finishedBets.Count,
            TotalPoints = betsWithPoints.Sum(b => b.PointsEarned),
            AveragePoints = betsWithPoints.Any()
                ? Math.Round(finishedBets.Average(b => b.PointsEarned), 2)
                : 0,
            ExactScores = betsWithPoints.Count(b => b.PointsEarned == 5),
            CorrectResults = betsWithPoints.Count(b => b.PointsEarned >= 3),
            CorrectGoalDifferences = betsWithPoints.Count(b => b.PointsEarned >= 1)
        };
    }
    
    private static string? GetPointsDetail(Domain.Entities.Bet bet)
    {
        return bet.PointsEarned switch
        {
            5 => "Score exact (+5)",
            4 => "Résultat + Diff. de buts (+4)",
            3 => "Bon résultat (+3)",
            1 => "Bonne diff. de buts (+1)",
            0 => "Aucun point (0)",
            _ => $"+{bet.PointsEarned}"
        };
    }
}