namespace BetAt.Domain.Entities;

public class BetRule : BaseEntity
{
    public int LeagueId { get; set; }
    public int ExactScorePoints { get; set; }
    public int CorrectResultPoints { get; set; }
    public int CorrectGoalDiffPoints { get; set; }

    public League League { get; set; } = null!;
}