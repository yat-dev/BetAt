namespace BetAt.Application.Features.Bet.Commands;

public class CalculateMatchPointsCommandHandler(
    IMatchRepository matchRepository, 
    IBetRepository betRepository,
    IPointsCalculationService pointsCalculationService,
    ILogger<CalculateMatchPointsCommandHandler> logger)
    : IRequestHandler<CalculateMatchPointsCommand, CalculateMatchPointsResultDto>
{
    public async Task<CalculateMatchPointsResultDto> Handle(CalculateMatchPointsCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("🎯 Calcul des points pour le match {MatchId}", request.MatchId);

        var match = await matchRepository.GetByIdAsync(request.MatchId);

        if (match == null)
        {
            logger.LogWarning("⚠️ Match {MatchId} non trouvé", request.MatchId);
            throw new NotFoundException("Match non trouvé");
        }

        if (match.PointsCalculated)
        {
            logger.LogInformation("ℹ️ Points déjà calculés pour le match {MatchId}", request.MatchId);
            return new CalculateMatchPointsResultDto { MatchId = match.Id, TotalBetsProcessed = 0 };
        }

        var bets = await betRepository.GetAllByMatchIdAsync(request.MatchId);
        
        logger.LogInformation("📊 {BetsCount} paris à traiter pour le match {MatchId}", bets.Count, request.MatchId);
        
        var result = new CalculateMatchPointsResultDto
        {
            MatchId = match.Id,
            TotalBetsProcessed = bets.Count
        };
        
        foreach (var bet in bets)
        {
            var points = pointsCalculationService.CalculatePoints(
                bet.PredictedHomeScore,
                bet.PredictedAwayScore,
                match.HomeScore!.Value,
                match.AwayScore!.Value
            );

            bet.PointsEarned = points;
            bet.IsProcessed = true;

            await betRepository.UpdateAsync(bet);
            
            // result.BetsUpdated.Add(new BetPointsDto
            // {
            //     BetId = bet.Id,
            //     UserId = bet.UserId,
            //     Points = points
            // });

            logger.LogDebug("✅ Pari {BetId} - User {UserId} : {Points} points", 
                bet.Id, bet.UserId, points);
        }
        
        match.PointsCalculated = true;
        
        await matchRepository.UpdateAsync(match);
        
        logger.LogInformation("✅ Points calculés avec succès pour {BetsCount} paris sur le match {MatchId}", bets.Count, request.MatchId);

        return result;
    }
}