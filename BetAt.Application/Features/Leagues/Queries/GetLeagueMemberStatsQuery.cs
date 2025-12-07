namespace BetAt.Application.Features.Leagues.Queries;

public class GetLeagueMemberStatsQuery : IRequest<List<LeagueMemberStatsDto>>
{
    public int LeagueId { get; set; }
}