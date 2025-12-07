namespace BetAt.Application.Features.Leagues.Queries;

public class GetLeagueMemberStatsQueryHandler(ILeagueRepository leagueRepository, IBetRepository betRepository) : IRequestHandler<GetLeagueMemberStatsQuery, List<LeagueMemberStatsDto>>
{
    public async Task<List<LeagueMemberStatsDto>> Handle(GetLeagueMemberStatsQuery request, CancellationToken cancellationToken)
    {
        var league = await leagueRepository.GetLeagueByIdAsync(request.LeagueId);

        if (league == null)
        {
            throw new NotFoundException("League not found");
        }
        
        var memberStats = new List<LeagueMemberStatsDto>();

        foreach (var member in league.Members)
        {
            var bets = await betRepository.GetAllByUserIdAsync(member.UserId);

            var totalBets = bets.Count;
            var processedBets = bets.Where(b => b.IsProcessed).ToList();
            var wonBets = processedBets.Count(b => b.PointsEarned > 0);
            var lostBets = processedBets.Count(b => b.PointsEarned == 0);
            var winRate = totalBets > 0 ? (decimal)wonBets / totalBets * 100 : 0;
            var averagePoints = totalBets > 0 ? (decimal)processedBets.Sum(b => b.PointsEarned) / totalBets : 0;
            
            var exactScores = processedBets.Count(b => b.PointsEarned == league.BetRule?.ExactScorePoints);
            var correctResults = processedBets.Count(b => b.PointsEarned == league.BetRule?.CorrectResultPoints);
            var correctGoalDifferences = processedBets.Count(b => b.PointsEarned == league.BetRule?.CorrectGoalDiffPoints);
            
            var lastResults = processedBets
                .Take(5)
                .Select(b => new BetResultDto
                {
                    IsWon = b.PointsEarned > 0,
                    Points = b.PointsEarned,
                    BetDate = b.CreatedAt
                })
                .ToList();
            
            var (currentStreak, isWinStreak) = CalculateCurrentStreak(processedBets);
            
            var currentRank = league.Members
                .OrderByDescending(m => m.Points)
                .ToList()
                .FindIndex(m => m.UserId == member.UserId) + 1;
            
            var previousRank = currentRank; // À implémenter avec un système d'historique
            var rankChange = previousRank - currentRank;
            var isNew = totalBets <= 3; // Considéré comme nouveau si moins de 3 paris
            
            memberStats.Add(new LeagueMemberStatsDto
            {
                UserId = member.UserId,
                DisplayName = member.User?.Username ?? "Unknown",
                CurrentPoints = member.Points,
                CurrentRank = currentRank,
                PreviousRank = previousRank,
                RankChange = rankChange,
                IsNew = isNew,
                TotalBets = totalBets,
                WonBets = wonBets,
                LostBets = lostBets,
                WinRate = Math.Round(winRate, 1),
                AveragePoints = Math.Round(averagePoints, 1),
                LastResults = lastResults,
                CurrentStreak = currentStreak,
                IsWinStreak = isWinStreak,
                ExactScores = exactScores,
                CorrectResults = correctResults,
                CorrectGoalDifferences = correctGoalDifferences
            });
        }
        
        return memberStats.OrderBy(s => s.CurrentRank).ToList();
    }
    
    private (int streak, bool isWin) CalculateCurrentStreak(List<Domain.Entities.Bet> bets)
    {
        if (!bets.Any()) return (0, false);

        var streak = 0;
        var firstBet = bets.First();
        var isWinStreak = firstBet.PointsEarned > 0;

        foreach (var bet in bets)
        {
            var isWin = bet.PointsEarned > 0;
            
            if (isWin == isWinStreak)
            {
                streak++;
            }
            else
            {
                break;
            }
        }

        return (streak, isWinStreak);
    }
}