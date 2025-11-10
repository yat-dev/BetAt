using BetAt.Domain.Repositories;

namespace BetAt.Infrastructure.Repositories;

public class LeagueMemberRepository(BetAtDbContext context) : ILeagueMemberRepository
{
    public async Task<List<LeagueMember>> GetAllAsync()
    {
        return await context.LeagueMembers.ToListAsync();
    }

    public async Task<int> GetAllByUserIdAsync(int userId)
    {
        return await context.LeagueMembers.Where(m => m.UserId == userId).CountAsync();
    }

    public async Task<int> GetAllPointsByUserIdAsync(int userId)
    {
        return await context.LeagueMembers.Where(m => m.UserId == userId).SumAsync(m => m.Points);
    }
}