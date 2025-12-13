using BetAt.Application.Common.Exceptions;

namespace BetAt.Infrastructure.Repositories;

public class TeamRepository(BetAtDbContext context) : ITeamRepository
{
    public async Task<List<Team>> GetAllAsync()
    {
        return await context.Team.ToListAsync();
    }

    public async Task<List<Team>> GetAllAsync(string? searchTerm, string? country)
    {
        var query = context.Team.AsQueryable();
        
        if (string.IsNullOrWhiteSpace(searchTerm) == false)
        {
            query = query.Where(v => v.Name.Contains(searchTerm));
        }

        if (string.IsNullOrWhiteSpace(country) == false)
        {
            query = query.Where(v => v.Country == country);
        }
        
        return await query.OrderBy(v => v.Name).ToListAsync();
    }

    public async Task<Team?> GetByIdAsync(int id)
    {
        return await context.Team.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(Team team)
    {
        await context.Team.AddAsync(team);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Team team)
    {
        context.Team.Update(team);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Team team)
    {
        var teamToDelete = context.Team.FirstOrDefault(x => x.Id == team.Id);
        
        if (teamToDelete == null)
            throw new NotFoundException($"Team with id {team.Id} does not exist.");
        
        context.Team.Remove(teamToDelete);
        await context.SaveChangesAsync();
    }

    public async Task<bool> HasMatchAsync(Team team)
    {
        return await context.Matches.AnyAsync(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id);
    }
}