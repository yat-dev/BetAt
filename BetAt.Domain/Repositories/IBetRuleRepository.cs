namespace BetAt.Domain.Repositories;

public interface IBetRuleRepository
{
    Task<List<BetRule>> GetAllAsync();

    Task<BetRule?> GetByIdAsync(int id);
    
    Task<BetRule> AddAsync(BetRule entity);
}