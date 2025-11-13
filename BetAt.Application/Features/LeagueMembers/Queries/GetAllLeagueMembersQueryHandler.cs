using BetAt.Domain.Entities;
using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.LeagueMembers.Queries;

public class GetAllLeagueMembersQueryHandler(ILeagueMemberRepository repository) : IRequestHandler<GetAllLeagueMembersQuery, List<LeagueMemberDto>>
{
    public async Task<List<LeagueMemberDto>> Handle(GetAllLeagueMembersQuery request, CancellationToken cancellationToken)
    {
        var leagueMembers = await repository.GetAllAsync();

        return leagueMembers.Select(lm => new LeagueMemberDto
        {
            Id = lm.Id,
            LeagueId = lm.LeagueId,
            UserId = lm.UserId,
            JoinedAt = lm.JoinedAt,
            MemberRole = lm.Role,
            Points = lm.Points,
            CreatedAt = lm.CreatedAt
        }).ToList();
    }
}