using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Mapping;

public static class MatchMappingExtensions
{
    public static MatchDto ToDto(this Match match)
    {
        return new MatchDto
        {
            Id = match.Id,
            HomeTeam = match.HomeTeam.ToDto(),
            AwayTeam = match.AwayTeam.ToDto(),
            Competition = match.Competition,
            MatchDate = match.MatchDate,
            HomeScore = match.HomeScore,
            AwayScore = match.AwayScore,
            Venue = new VenueDto
            {
                Id = match.Venue.Id,
                Name = match.Venue.Name,
                Capacity = match.Venue.Capacity,
                Country = match.Venue.Country,
                ImageUrl = match.Venue.ImageUrl,
                City = match.Venue.City
            },
        };
    }
}