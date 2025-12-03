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

    public async Task<Match> GetByIdAsync(int id)
    {
        return (await context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Venue) // ← Ajouter cette ligne
            .FirstOrDefaultAsync(m => m.Id == id))!;
    }
    
    public async Task UpdateAsync(Match match)
    {
        context.Matches.Update(match);
        await context.SaveChangesAsync();
    }
}