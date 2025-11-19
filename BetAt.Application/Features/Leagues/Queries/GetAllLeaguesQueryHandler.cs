using BetAt.Application.Mapping;
using BetAt.Domain.Entities;
using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.Leagues.Queries;

public class GetAllLeaguesQueryHandler(ILeagueRepository repository) : IRequestHandler<GetAllLeaguesQuery, List<LeagueDto>>
{
    public async Task<List<LeagueDto>> Handle(GetAllLeaguesQuery request, CancellationToken cancellationToken)
    {
        var leagues = await repository.GetAllLeaguesAsync();

        return leagues.Select(l => l.ToDto()).ToList();
    }
}