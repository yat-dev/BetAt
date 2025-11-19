using BetAt.Domain.Repositories;

namespace BetAt.Infrastructure.Repositories;

public class LeagueRepository(BetAtDbContext context) : ILeagueRepository
{
    public async Task<List<League>> GetAllLeaguesAsync()
    {
        return await context.Leagues.ToListAsync();
    }
    
    public async Task<List<League>> GetMyLeaguesAsync(int userId)
    {
        var leagueMembers = await context.LeagueMembers
            .Where(lm => lm.UserId == userId)
            .Include(l => l.League)
            .Include(l => l.User)
            .ToListAsync();
        
        return leagueMembers.Select(lm => lm.League).ToList();
    }

    public async Task<League?> GetLeagueByIdAsync(int id)
    {
        return await context.Leagues
            .Include(l => l.CreatedBy)
            .Include(l => l.Members)
                .ThenInclude(m => m.User)
            .Include(l => l.BetRule)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<League?> GetLeagueByNameAsync(string name)
    {
        return await context.Leagues.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<League?> GetLeagueByCodeAsync(string code)
    {
        return await context.Leagues.FirstOrDefaultAsync(x => x.Code == code);
    }

    public Task<bool> CodeExistsAsync(string code)
    {
        return context.Leagues.AnyAsync(x => x.Code == code);
    }

    public async Task<League> AddAsync(League league)
    {
        context.Leagues.Add(league);
        await context.SaveChangesAsync();
        return league;
    }


}