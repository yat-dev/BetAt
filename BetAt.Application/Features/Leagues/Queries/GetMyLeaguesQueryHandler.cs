using BetAt.Application.Mapping;

namespace BetAt.Application.Features.Leagues.Queries;

public class GetMyLeaguesQueryHandler(ILeagueRepository repository, ICurrentUserService userService) : IRequestHandler<GetMyLeaguesQuery, List<LeagueDto>>
{
    public async Task<List<LeagueDto>> Handle(GetMyLeaguesQuery request, CancellationToken cancellationToken)
    {
        var leagues = await repository.GetMyLeaguesAsync(userService.UserId);
        
        return leagues.Select(l => l.ToDto()).ToList();
    }
}