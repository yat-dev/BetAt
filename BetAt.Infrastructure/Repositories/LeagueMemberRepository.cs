using BetAt.Domain.Repositories;

namespace BetAt.Infrastructure.Repositories;

public class LeagueMemberRepository(BetAtDbContext context, ILogger<LeagueMemberRepository> logger) : ILeagueMemberRepository
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
        context.LeagueMembers.AddAsync(leagueMember);
        await context.SaveChangesAsync();
        return leagueMember;
    }

    public async Task UpdatePointsAsync(int leagueId, int userId, int points)
    {
        var leagueMember = await context.LeagueMembers
            .FirstOrDefaultAsync(lm => lm.LeagueId == leagueId && lm.UserId == userId);
        
        if (leagueMember == null)
        {
            logger.LogWarning("⚠️ LeagueMember non trouvé - LeagueId={LeagueId}, UserId={UserId}",
                leagueId, userId);
            return;
        }
        
        leagueMember.Points += points;
        
        context.LeagueMembers.Update(leagueMember);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(LeagueMember leagueMember)
    {
        context.LeagueMembers.Remove(leagueMember);
        await context.SaveChangesAsync();
    }
}