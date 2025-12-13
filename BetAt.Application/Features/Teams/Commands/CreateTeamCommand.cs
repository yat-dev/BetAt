namespace BetAt.Application.Features.Teams.Commands;

public class CreateTeamCommand : IRequest<TeamDto>
{
    public CreateTeamDto Dto { get; set; } = null!;
}