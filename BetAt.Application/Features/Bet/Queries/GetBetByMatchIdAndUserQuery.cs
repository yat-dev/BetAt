namespace BetAt.Application.Features.Bet.Queries;

public class GetBetByMatchIdAndUserQuery : IRequest<BetDto?>
{
    public int MatchId { get; set; }
}