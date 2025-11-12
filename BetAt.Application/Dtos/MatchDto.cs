namespace BetAt.Application.Dtos;

public class MatchDto
{
    public int Id { get; set; }
    public TeamDto HomeTeam { get; set; } = null!;
    public TeamDto AwayTeam { get; set; } = null!;
    public string Competition { get; set; } = string.Empty;
    public DateTimeOffset MatchDate { get; set; }
    public Status Status { get; set; } = Status.Scheduled;
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
}