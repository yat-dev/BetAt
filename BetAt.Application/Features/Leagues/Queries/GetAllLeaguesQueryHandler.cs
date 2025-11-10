using BetAt.Domain.Entities;
using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.Leagues.Queries;

public class GetAllLeaguesQueryHandler(ILeagueRepository repository, ICurrentUserService currentUserService) : IRequestHandler<GetAllLeaguesQuery, List<LeagueDto>>
{
    public async Task<List<LeagueDto>> Handle(GetAllLeaguesQuery request, CancellationToken cancellationToken)
    {
        var leagues = await repository.GetAllLeaguesAsync();

        return leagues.Select(l => new LeagueDto
        {
            Id = l.Id,
            Name = l.Name,
            Description = l.Description,
            Code = l.Code,
            CreatedById = l.CreatedById,
            IsActive = l.IsActive,
            CreatedAt = l.CreatedAt
        }).ToList();
    }
}