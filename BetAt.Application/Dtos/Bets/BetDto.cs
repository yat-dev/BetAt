using BetAt.Application.Dtos.Matches;

namespace BetAt.Application.Dtos;

public class BetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public MatchDto Match { get; set; } = new MatchDto();
    public LeagueDto League { get; set; } = new LeagueDto();
    public int PredictedHomeScore { get; set; }
    public int PredictedAwayScore { get; set; }
    public int PointsEarned { get; set; }
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
    public bool IsProcessed { get; set; }
    public string? PointsDetail { get; set; }
}