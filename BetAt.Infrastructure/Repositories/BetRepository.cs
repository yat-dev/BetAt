using BetAt.Domain.Enum;
using BetAt.Infrastructure;

namespace BetAt.Infrastructure.Repositories;

public class BetRepository(BetAtDbContext context) : IBetRepository
{
    public async Task<List<Bet>> GetAllAsync()
    {
        return await context.Bets.ToListAsync();
    }

    public async Task<List<Bet>> GetAllByLeagueIdAndStatusAsync(int userId, int? leagueId, BetStatus status)
    {
        return await context.Bets
            .Include(b => b.Match)
                .ThenInclude(m => m.HomeTeam)
            .Include(b => b.Match)
                .ThenInclude(m => m.AwayTeam)
            .Include(b => b.Match)
                .ThenInclude(b => b.Venue)
            .Include(b => b.League)
            .Where(b => b.UserId == userId)
            .Where(b => !leagueId.HasValue || b.LeagueId == leagueId.Value)
            .Where(b => status == BetStatus.All || 
                        (status == BetStatus.Finished && b.IsProcessed) ||
                        (status == BetStatus.Pending && !b.IsProcessed))
            .ToListAsync();
    }

    public async Task<List<Bet>> GetAllByUserIdAsync(int userId)
    {
        return await context.Bets
            .Include(b => b.Match)
                .ThenInclude(m => m.Venue)
            .Include(b => b.Match)
                .ThenInclude(m => m.HomeTeam)
            .Include(b => b.Match)
                .ThenInclude(m => m.AwayTeam)            
            .Include(b => b.League)
            .Where(b => b.UserId == userId).ToListAsync();
    }
    
    public async Task<List<Bet>> GetAllByMatchIdAsync(int matchId)
    {
        return await context.Bets.Where(b => b.MatchId == matchId).ToListAsync();
    }

    public async Task<Bet?> GetByIdAsync(int matchId, int userId)
    {
        var res = await context.Bets
            .Include(b => b.User)
            .Include(b => b.Match)
                .ThenInclude(m => m.HomeTeam)
            .Include(b => b.Match)
                .ThenInclude(m => m.AwayTeam)
            .Include(b => b.League)
            .Include(b => b.Match.Venue)
            .Where(b => b.Match.Id == matchId)
            .Where(b => b.UserId == userId)
            .FirstOrDefaultAsync();

        return res;
    }

    public int GetCountByMatchIdAsync(int matchId)
    {
        return context.Bets.Select(m => m.MatchId == matchId).ToList().Count;
    }

    public Task<bool> IsMatchHasBetAsync(int matchId)
    {
        return context.Bets.AnyAsync(b => b.MatchId == matchId && b.IsProcessed && b.PointsEarned > 0);
    }

    public async Task<Bet> AddAsync(Bet bet)
    {
        context.Bets.Add(bet);
        await context.SaveChangesAsync();
        return bet;
    }

    public async Task UpdateAsync(Bet bet)
    {
        context.Bets.Update(bet);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Bet bet)
    {
        context.Bets.Remove(bet);
        await context.SaveChangesAsync();
    }
}