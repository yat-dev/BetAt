using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.Matches.Queries;

public class GetAllUpcomingMatchesQueryHandler(IMatchRepository repository) : IRequestHandler<GetAllUpcomingMatchesQuery, List<MatchDto>>
{
    public async Task<List<MatchDto>> Handle(GetAllUpcomingMatchesQuery request, CancellationToken cancellationToken)
    {
        var matches = await repository.GetAllUpcomingAsync(request.Days);

        return matches.Select(m => new MatchDto
        {
            Id = m.Id,
            HomeTeam = new TeamDto
            {
                Id = m.HomeTeam.Id,
                Name = m.HomeTeam.Name,
                ShortName = m.HomeTeam.ShortName,
                LogoUrl = m.HomeTeam.LogoUrl,
                Country = m.HomeTeam.Country ?? string.Empty
            },
            AwayTeam = new TeamDto
            {
                Id = m.AwayTeamId,
                Name = m.AwayTeam.Name,
                ShortName = m.AwayTeam.ShortName,
                LogoUrl = m.AwayTeam.LogoUrl,
                Country = m.AwayTeam.Country ?? string.Empty
            },
            Competition = m.Competition,
            MatchDate = m.MatchDate,
            Status = m.Status,
            HomeScore = m.HomeScore,
            AwayScore = m.AwayScore
        }).ToList();
    }
}