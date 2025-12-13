namespace BetAt.Application.Features.Teams.Queries;

public class GetTeamByIdQuery : IRequest<TeamDto>
{
    public int Id { get; set; }
}