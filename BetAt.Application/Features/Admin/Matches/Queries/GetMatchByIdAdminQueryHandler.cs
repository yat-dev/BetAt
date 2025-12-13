using BetAt.Application.Dtos.Matches;
using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Admin.Matches.Queries;

public class GetMatchByIdAdminQueryHandler(IMatchRepository matchRepository, IBetRepository betRepository) : IRequestHandler<GetMatchByIdAdminQuery, MatchDto>
{
    public async Task<MatchDto> Handle(GetMatchByIdAdminQuery request, CancellationToken cancellationToken)
    {
        var match = await matchRepository.GetByIdAsync(request.Id);
        
        if (match == null)
            throw new NotFoundException($"Match {request.Id} not found.");
        
        return new MatchDto
        {
            Id = match.Id,
            HomeTeam = new TeamDto
            {
                Id = match.HomeTeam.Id,
                Name = match.HomeTeam.Name,
                Country = match.HomeTeam.Country,
                LogoUrl = match.HomeTeam.LogoUrl,
                ShortName = match.HomeTeam.ShortName
            },
            AwayTeam = new TeamDto
            {
                Id = match.AwayTeam.Id,
                Name = match.AwayTeam.Name,
                Country = match.AwayTeam.Country,
                LogoUrl = match.AwayTeam.LogoUrl,
                ShortName = match.AwayTeam.ShortName
            },
            Venue = new VenueDto
            {
                Id = match.Venue!.Id,
                Name = match.Venue.Name,
                Country = match.Venue.Country,
                City = match.Venue.City,
                Capacity = match.Venue.Capacity,
                ImageUrl = match.Venue.ImageUrl
            },
            Competition = match.Competition,
            MatchDate = match.MatchDate,
            HomeScore = match.HomeScore,
            AwayScore = match.AwayScore,
            MatchStatus = match.Status,
            StatusLabel = match.Status.ToString(),
            BetCount = betRepository.GetCountByMatchIdAsync(match.Id)
        };
    }
}