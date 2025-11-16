using BetAt.Application.Common.Exceptions;
using BetAt.Application.Dtos.BetRules;
using BetAt.Domain.Entities;
using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.Leagues.Queries;

public class GetLeagueByIdQueryHandler(ILeagueRepository repository, ICurrentUserService currentUserService) : IRequestHandler<GetLeagueByIdQuery, LeagueDetailDto?>
{
    public async Task<LeagueDetailDto?> Handle(GetLeagueByIdQuery request, CancellationToken cancellationToken)
    {
        var league = await repository.GetLeagueByIdAsync(request.Id);

        if (league == null)
            throw new NotFoundException(nameof(League), request.Id);
        
        var currentUserId = currentUserService.UserId;
        
        var currentMember = league.Members.FirstOrDefault(m => m.UserId == currentUserId);
        var isUserAdmin = currentMember?.Role == MemberRole.Admin;

        return new LeagueDetailDto
        {
            Id = league.Id,
            Name = league.Name,
            Description = league.Description,
            Code = league.Code,
            CreatedById = league.CreatedById,
            CreatedByName = league.CreatedBy.DisplayName, 
            CreatedAt = league.CreatedAt,
            IsActive = league.IsActive,
            IsUserAdmin = isUserAdmin,
            Members = league.Members
                .OrderByDescending(m => m.Points)
                .ThenBy(m => m.JoinedAt)
                .Select(m => new LeagueMemberDto
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    LeagueId = m.LeagueId,
                    UserName = m.User.Username,
                    Points = m.Points,
                    DisplayName = m.User.DisplayName,
                    JoinedAt = m.JoinedAt,
                    MemberRole = m.Role,
                    CreatedAt = m.CreatedAt
                }).ToList(),
            BetRule = league.BetRule != null ? new BetRuleDto
            {
                LeagueId = league.Id,
                ExactScorePoints = league.BetRule.ExactScorePoints,
                CorrectResultPoints = league.BetRule.CorrectResultPoints,
                CorrectGoalDiffPoints = league.BetRule.CorrectGoalDiffPoints
            } : null
        };
    }
}