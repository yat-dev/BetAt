namespace BetAt.Domain.Entities;

public class Bet : BaseEntity
{
    public int UserId { get; set; }
    public int MatchId { get; set; }
    public int LeagueId { get; set; }
    public int PredictedHomeScore { get; set; }
    public int PredictedAwayScore { get; set; }
    public int PointsEarned { get; set; }
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
    public bool IsProcessed { get; set; }
    
    public User User { get; set; } = null!;
    public Match Match { get; set; } = null!;
    public League League { get; set; } = null!;
}