using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.LeagueMembers.Queries;

public class GetAllLeaguesMemberByUserIdQueryHandler(ILeagueMemberRepository repository, ICurrentUserService userService) : IRequestHandler<GetAllLeaguesMemberByUserIdQuery, int>
{
    public async Task<int> Handle(GetAllLeaguesMemberByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllByUserIdAsync(userService.UserId);
    }
}