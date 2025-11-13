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
            AwayScore = match.AwayScore
        };
    }
}