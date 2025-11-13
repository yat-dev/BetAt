namespace BetAt.Application.Features.LeagueMembers.Commands;

public class DeleteLeagueMemberCommand : IRequest
{
    public int LeagueId { get; set; }
}