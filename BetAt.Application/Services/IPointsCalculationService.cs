namespace BetAt.Application.Services;

public interface IPointsCalculationService
{
    int CalculatePoints(int predictedHomeScore, int predictedAwayScore, int actualHomeScore, int actualAwayScore);
}