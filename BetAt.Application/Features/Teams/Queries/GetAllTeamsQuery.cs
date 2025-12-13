namespace BetAt.Application.Features.Teams.Queries;

public class GetAllTeamsQuery : IRequest<List<TeamDto>>
{
    public string? SearchTerm { get; set; }
    public string? Country { get; set; }
}