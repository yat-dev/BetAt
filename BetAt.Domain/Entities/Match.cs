namespace BetAt.Domain.Entities;

public class Match : BaseEntity
{
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public string Competition { get; set; } = string.Empty;
    public DateTimeOffset MatchDate { get; set; }
    public Status Status { get; set; } = Status.Scheduled;
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public int? ExternalApiId { get; set; }
    
    public Team HomeTeam { get; set; } = null!;
    public Team AwayTeam { get; set; } = null!;
    public ICollection<Bet> Bets { get; set; } = [];
}