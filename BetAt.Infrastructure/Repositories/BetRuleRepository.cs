namespace BetAt.Infrastructure.Repositories;

public class BetRuleRepository(BetAtDbContext context) : IBetRuleRepository
{
    public async Task<List<BetRule>> GetAllAsync()
    {
        return await context.BetRules.ToListAsync();
    }

    public async Task<BetRule?> GetByIdAsync(int id)
    {
        return await context.BetRules.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BetRule> AddAsync(BetRule entity)
    {
        await context.BetRules.AddAsync(entity);
        await context.SaveChangesAsync();
        
        return entity;
    }
}