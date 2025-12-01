namespace BetAt.Application.Services;

public class PointsCalculationService(ILogger<PointsCalculationService> logger) : IPointsCalculationService
{
    public int CalculatePoints(int predictedHomeScore, int predictedAwayScore, int actualHomeScore, int actualAwayScore)
    {
        int points = 0;

        if (predictedHomeScore == actualHomeScore && predictedAwayScore == actualAwayScore)
        {
            points = 5;
            logger.LogDebug("✅ Score exact : +5 points");
            return points;
        }
        
        var predictedResult = GetMatchResult(predictedHomeScore, predictedAwayScore);
        var actualResult = GetMatchResult(actualHomeScore, actualAwayScore);
        
        if (predictedResult == actualResult)
        {
            points += 3;
            logger.LogDebug("✅ Bon résultat : +3 points");
        }
        
        var predictedGoalDifference = predictedHomeScore - predictedAwayScore;
        var actualGoalDifference = actualHomeScore - actualAwayScore;

        bool isDraw = actualHomeScore - actualAwayScore == 0;
        
        if (predictedGoalDifference == actualGoalDifference && isDraw == false)
        {
            points += 1;
            logger.LogDebug("✅ Bonne différence de buts : +1 point");
        }

        logger.LogDebug("Total points : {Points}", points);
        return points;
    }
    
    private MatchResult GetMatchResult(int homeScore, int awayScore)
    {
        if (homeScore > awayScore) return MatchResult.HomeWin;
        if (homeScore < awayScore) return MatchResult.AwayWin;
        return MatchResult.Draw;
    }
}