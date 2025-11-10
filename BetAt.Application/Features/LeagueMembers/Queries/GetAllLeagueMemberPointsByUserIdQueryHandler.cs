using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.LeagueMembers.Queries;

public class GetAllLeagueMemberPointsByUserIdQueryHandler(ILeagueMemberRepository repository, ICurrentUserService userService) : IRequestHandler<GetAllLeagueMemberPointsByUserIdQuery, int>
{
    public async Task<int> Handle(GetAllLeagueMemberPointsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllPointsByUserIdAsync(userService.UserId);
    }
}