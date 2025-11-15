namespace BetAt.Application.Dtos;

public class CreateBetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MatchId { get; set; }
    public int LeagueId { get; set; }
    public int PredictedHomeScore { get; set; }
    public int PredictedAwayScore { get; set; }
    public int PointsEarned { get; set; }
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
    public bool IsProcessed { get; set; }
}