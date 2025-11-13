namespace BetAt.Application.Mapping;

public static class BetMappingExtensions
{
    public static BetDto ToDto(this Bet bet)
    {
        return new BetDto
        {
            Id = bet.Id,
            UserId = bet.UserId,
            Match = bet.Match.ToDto(),
            League = bet.League.ToDto(),
            PredictedHomeScore = bet.PredictedHomeScore,
            PredictedAwayScore = bet.PredictedAwayScore,
            PointsEarned = bet.PointsEarned,
            PlacedAt = bet.PlacedAt,
            IsProcessed = bet.IsProcessed
        };
    }
}