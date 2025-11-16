using BetAt.Domain.Repositories;

namespace BetAt.Infrastructure.Repositories;

public class LeagueMemberRepository(BetAtDbContext context) : ILeagueMemberRepository
{
    public async Task<List<LeagueMember>> GetAllAsync()
    {
        return await context.LeagueMembers
                .Include(u => u.User)
                .ToListAsync();
    }

    public async Task<int> GetCountByUserIdAsync(int userId)
    {
        return await context.LeagueMembers.Where(m => m.UserId == userId).CountAsync();
    }

    public async Task<int> GetAllPointsByUserIdAsync(int userId)
    {
        return await context.LeagueMembers.Where(m => m.UserId == userId).SumAsync(m => m.Points);
    }

    public async Task<LeagueMember?> GetByUserAndLeagueAsync(int userId, int leagueId)
    {
        return await context.LeagueMembers.Where(m => m.UserId == userId && m.LeagueId == leagueId).FirstOrDefaultAsync();
    }

    public async Task<int> GetMembersCountAsync(int leagueId)
    {
        return await context.LeagueMembers.Where(m => m.LeagueId == leagueId).CountAsync();
    }

    public async Task<LeagueMember> AddAsync(LeagueMember leagueMember)
    {
        await context.LeagueMembers.AddAsync(leagueMember);
        await context.SaveChangesAsync();
        return leagueMember;
    }
    
    public async Task DeleteAsync(LeagueMember leagueMember)
    {
        context.LeagueMembers.Remove(leagueMember);
        await context.SaveChangesAsync();
    }
}