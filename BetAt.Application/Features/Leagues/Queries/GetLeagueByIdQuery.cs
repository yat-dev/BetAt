namespace BetAt.Application.Features.Leagues.Queries;

public class GetLeagueByIdQuery : IRequest<LeagueDetailDto?>
{
    public int Id { get; set; }
}