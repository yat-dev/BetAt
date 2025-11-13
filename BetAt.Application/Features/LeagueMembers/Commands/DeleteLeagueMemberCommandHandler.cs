using BetAt.Application.Common.Exceptions;

namespace BetAt.Application.Features.LeagueMembers.Commands;

public class DeleteLeagueMemberCommandHandler(ILeagueMemberRepository leagueMemberRepository, ICurrentUserService userService) : IRequestHandler<DeleteLeagueMemberCommand> 
{
    public async Task Handle(DeleteLeagueMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await leagueMemberRepository.GetByUserAndLeagueAsync(userService.UserId, request.LeagueId);
        
        if (member == null)
            throw new NotFoundException("Vous n'êtes pas membre de cette ligue");
        
        if (member.Role == MemberRole.Admin)
        {
            var membersCount = await leagueMemberRepository.GetMembersCountAsync(request.LeagueId);
            
            if (membersCount > 1)
            {
                throw new BadRequestException("En tant qu'admin, vous devez d'abord transférer les droits ou supprimer la ligue");
            }
        }
        
        await leagueMemberRepository.DeleteAsync(member);
    }
}