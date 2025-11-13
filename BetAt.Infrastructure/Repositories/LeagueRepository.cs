using BetAt.Domain.Repositories;

namespace BetAt.Infrastructure.Repositories;

public class LeagueRepository(BetAtDbContext context) : ILeagueRepository
{
    public async Task<List<League>> GetAllLeaguesAsync()
    {
        return await context.Leagues.ToListAsync();
    }
    
    public async Task<List<League>> GetAllByUserIdAsync(int userId)
    {
        var leagueMembers = await context.LeagueMembers
            .Where(lm => lm.UserId == userId)
            .Include(l => l.League)
            .ToListAsync();
        
        return leagueMembers.Select(lm => lm.League).ToList();
    }

    public async Task<League?> GetLeagueByIdAsync(int id)
    {
        return await context.Leagues.FindAsync(id);
    }

    public async Task<League?> GetLeagueByNameAsync(string name)
    {
        return await context.Leagues.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<League?> GetLeagueByCodeAsync(string code)
    {
        return await context.Leagues.FirstOrDefaultAsync(x => x.Code == code);
    }
}