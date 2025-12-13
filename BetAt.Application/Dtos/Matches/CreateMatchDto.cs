namespace BetAt.Application.Dtos.Matches;

public class CreateMatchDto
{
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int VenueId { get; set; }
    public string Competition { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
}