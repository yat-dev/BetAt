namespace BetAt.Application.Dtos.BetRules;

public class BetRuleDto
{
    public int LeagueId { get; set; }
    public int ExactScorePoints { get; set; }
    public int CorrectResultPoints { get; set; }
    public int CorrectGoalDiffPoints { get; set; }
}