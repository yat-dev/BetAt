using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Teams.Commands;

public class UpdateTeamCommand : IRequest<TeamDto>
{
    public UpdateTeamDto Dto { get; set; } = null!;
}