namespace BetAt.Application.Features.Bet.Queries;

public class GetAllBetsQueryHandler(IBetRepository repository) : IRequestHandler<GetAllBetsQuery, List<BetDto>>
{
    public async Task<List<BetDto>> Handle(GetAllBetsQuery request, CancellationToken cancellationToken)
    {
        // var bets = await repository.GetAllAsync();
        //
        // return bets.Select(b => new BetDto
        // {
        //     Id = b.Id,
        //     UserId = b.UserId,
        //     MatchId = b.MatchId,
        //     LeagueId = b.LeagueId,
        //     PredictedHomeScore = b.PredictedHomeScore,
        //     PredictedAwayScore = b.PredictedAwayScore,
        //     PointsEarned = b.PointsEarned,
        //     IsProcessed = b.IsProcessed,
        //     PlacedAt = b.PlacedAt
        // }).ToList();

        return new List<BetDto>();
    }
}