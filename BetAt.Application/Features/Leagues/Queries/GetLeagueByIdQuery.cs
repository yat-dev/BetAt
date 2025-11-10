namespace BetAt.Application.Features.Leagues.Queries;

public class GetLeagueByIdQuery : IRequest<LeagueDto?>
{
    public int Id { get; set; }
}