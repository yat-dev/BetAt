namespace BetAt.Application.Features.Teams.Commands;

public class DeleteTeamCommand : IRequest
{
    public int Id { get; set; }
}