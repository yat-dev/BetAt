namespace BetAt.Application.Dtos.Matches;

public class UpdateMatchDto
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int VenueId { get; set; }
    public string Competition { get; set; } = string.Empty;
    public DateTimeOffset MatchDate { get; set; }
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public int Status { get; set; }
}