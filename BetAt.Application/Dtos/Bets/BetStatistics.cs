namespace BetAt.Application.Dtos;

public class BetStatistics
{
    public int TotalBets { get; set; }
    public int PendingBets { get; set; }
    public int FinishedBets { get; set; }
    public int TotalPoints { get; set; }
    public double AveragePoints { get; set; }
    public int ExactScores { get; set; }
    public int CorrectResults { get; set; }
    public int CorrectGoalDifferences { get; set; }
}