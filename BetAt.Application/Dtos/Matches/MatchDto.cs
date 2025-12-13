using BetAt.Application.Dtos.Teams;
using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Dtos.Matches;

public class MatchDto
{
    public int Id { get; set; }
    public TeamDto HomeTeam { get; set; } = null!;
    public TeamDto AwayTeam { get; set; } = null!;
    public string Competition { get; set; } = string.Empty;
    public DateTimeOffset MatchDate { get; set; }
    public MatchStatus MatchStatus { get; set; } = MatchStatus.Scheduled;
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public VenueDto? Venue { get; set; }
    public string StatusLabel { get; set; } = string.Empty;
    public int BetCount { get; set; }
}