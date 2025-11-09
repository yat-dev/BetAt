namespace BetAt.Domain.Entities;

public class Match : BaseEntity
{
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;
    public string Competition { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public Status Status { get; set; } = Status.Scheduled;
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    
    public ICollection<Bet> Bets { get; set; } = [];
}