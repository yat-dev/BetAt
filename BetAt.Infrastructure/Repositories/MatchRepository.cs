using BetAt.Domain.Enum;
using BetAt.Domain.Repositories;

namespace BetAt.Infrastructure.Repositories;

public class MatchRepository(BetAtDbContext context) : IMatchRepository
{
    public async Task<List<Match>> GetAllAsync()
    {
        return await context.Matches.ToListAsync();
    }

    public async Task<List<Match>> GetAllUpcomingAsync(int days)
    {
        return await context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => m.Status == MatchStatus.Scheduled)
            .Where(m => m.MatchDate > DateTimeOffset.UtcNow && m.MatchDate < DateTimeOffset.UtcNow.AddDays(days))
            .OrderBy(m => m.MatchDate)
            .ToListAsync();
    }

    public async Task<List<Match>> GetAllLivesAsync()
    {
        return await context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => m.Status == MatchStatus.Live)
            .OrderBy(m => m.MatchDate)
            .ToListAsync();
    }

    public async Task<List<Match>> GetAllFinishedMatchesAsync()
    {
        return await context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => m.Status == MatchStatus.Finished)
            .OrderBy(m => m.MatchDate)
            .ToListAsync();
    }

    public async Task<Match> GetByIdAsync(int id)
    {
        return (await context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Venue)
            .FirstOrDefaultAsync(m => m.Id == id))!;
    }

    public Task<bool> IsMatchExistsAsync(int homeTeamId, int awayTeamId, DateTime matchDate)
    {
        return context.Matches.AnyAsync(m => m.HomeTeamId == homeTeamId &&
                                                   m.AwayTeamId == awayTeamId &&
                                                   m.MatchDate.Date == matchDate.Date);
    }

    public async Task<Match> AddAsync(Match match)
    {
        context.Matches.Add(match);
        await context.SaveChangesAsync();
        return match;
    }

    public async Task UpdateAsync(Match match)
    {
        context.Matches.Update(match);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Match match)
    {
        context.Matches.Remove(match);
        var result = await context.SaveChangesAsync();
        
        return result > 0;
    }
}