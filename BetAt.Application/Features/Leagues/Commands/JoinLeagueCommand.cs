namespace BetAt.Application.Features.Leagues.Commands;

public class JoinLeagueCommand : IRequest<LeagueDto>
{
    public string Code { get; set; } = string.Empty;
}