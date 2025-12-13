using BetAt.Application.Dtos.Matches;
using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Admin.Matches.Queries;

public class GetAllMatchesAdminQueryHandler(IMatchRepository repository, IBetRepository betRepository) : IRequestHandler<GetAllMatchesAdminQuery, List<MatchDto>>
{
    public async Task<List<MatchDto>> Handle(GetAllMatchesAdminQuery request, CancellationToken cancellationToken)
    {
        var matches = await repository.GetAllAsync();

        if (string.IsNullOrWhiteSpace(request.Competition) == false)
        {
            matches = matches.Where(m => m.Competition == request.Competition).ToList();
        }

        if (request.Status.HasValue)
        {
            matches = matches.Where(m => m.Status == request.Status.Value).ToList();
        }

        if (request.FromDate.HasValue)
        {
            matches = matches.Where(m => m.CreatedAt >= request.FromDate).ToList();
        }

        if (request.ToDate.HasValue)
        {
            matches = matches.Where(m => m.CreatedAt <= request.ToDate).ToList();
        }
        
        return matches.Select(m => new MatchDto
        {
            Id = m.Id,
            HomeTeam = new TeamDto
            {
                Id = m.HomeTeam.Id,
                Name = m.HomeTeam.Name,
                Country = m.HomeTeam.Country,
                LogoUrl = m.HomeTeam.LogoUrl,
                ShortName = m.HomeTeam.ShortName
            },
            AwayTeam = new TeamDto
            {
                Id = m.AwayTeam.Id,
                Name = m.AwayTeam.Name,
                Country = m.AwayTeam.Country,
                LogoUrl = m.AwayTeam.LogoUrl,
                ShortName = m.AwayTeam.ShortName
            },
            Venue = new VenueDto
            {
                Id = m.Venue!.Id,
                Name = m.Venue.Name,
                Country = m.Venue.Country,
                City = m.Venue.City,
                Capacity = m.Venue.Capacity,
                ImageUrl = m.Venue.ImageUrl
            },
            Competition = m.Competition,
            MatchDate = m.MatchDate,
            HomeScore = m.HomeScore,
            AwayScore = m.AwayScore,
            MatchStatus = m.Status,
            StatusLabel = m.Status.ToString(),
            BetCount = betRepository.GetCountByMatchIdAsync(m.Id)
        }).ToList();
    }
}