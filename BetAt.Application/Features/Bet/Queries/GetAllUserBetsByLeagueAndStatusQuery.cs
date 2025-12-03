namespace BetAt.Application.Features.Bet.Queries;

public class GetAllUserBetsByLeagueAndStatusQuery : IRequest<List<BetDto>>
{
    public int? LeagueId { get; set; }
    public BetStatus Status { get; set; }
}