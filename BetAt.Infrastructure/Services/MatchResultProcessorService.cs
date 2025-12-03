using BetAt.Application.Features.Bet.Commands;
using BetAt.Domain.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;

namespace BetAt.Infrastructure.Services;

public class MatchResultProcessorService(IServiceProvider serviceProvider, ILogger<MatchResultProcessorService> logger, IConfiguration configuration) : BackgroundService
{
    private TimeSpan _interval = TimeSpan.FromMinutes(5);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var intervalMinutesSettings = configuration.GetSection("BackgroundJobs:MatchResultProcessor");
            var isActive = bool.Parse(intervalMinutesSettings["IsActive"]!);
            
            if (isActive == false)
                return;
            
            var intervalMinutes = long.Parse(intervalMinutesSettings["IntervalMinutes"]!);
            _interval = TimeSpan.FromMinutes(intervalMinutes);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            logger.LogInformation("🚀 MatchResultProcessorService démarré - Intervalle: {Interval}", _interval);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessFinishedMatches(stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "❌ Erreur lors du traitement des matchs terminés");
                }

                await Task.Delay(_interval, stoppingToken);
            }

            logger.LogInformation("🛑 MatchResultProcessorService arrêté");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Erreur lors du traitement");
        }
    }

    private async Task ProcessFinishedMatches(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BetAtDbContext>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        
        var finishedMatches = await context.Matches
            .Where(m => m.Status == MatchStatus.Finished 
                        && m.HomeScore != null 
                        && m.AwayScore != null
                        && m.PointsCalculated == false)
            .OrderBy(m => m.MatchDate) // Traiter les plus anciens en premier
            .Take(10) // Max 10 matchs à la fois (éviter de surcharger)
            .ToListAsync(cancellationToken);
        
        if (finishedMatches.Any() == false)
        {
            logger.LogDebug("ℹ️ Aucun match à traiter");
            return;
        }

        logger.LogInformation("🎯 {Count} matchs terminés à traiter", finishedMatches.Count);
        
        foreach (var match in finishedMatches)
        {
            try
            {
                var command = new CalculateMatchPointsCommand { MatchId = match.Id };
                var result = await mediator.Send(command, cancellationToken);

                logger.LogInformation(
                    "✅ Match {MatchId} traité : {BetsCount} paris, Points calculés",
                    match.Id, result.TotalBetsProcessed);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, 
                    "❌ Erreur lors du calcul des points pour le match {MatchId}", 
                    match.Id);
                
                continue;
            }
        }
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("🛑 Arrêt du MatchResultProcessorService...");
        await base.StopAsync(cancellationToken);
    }
}