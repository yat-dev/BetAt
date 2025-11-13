namespace BetAt.Domain.Repositories;

public interface ILeagueMemberRepository
{
    Task<List<LeagueMember>> GetAllAsync();
    
    Task<int> GetCountByUserIdAsync(int userId);
    
    Task<int> GetAllPointsByUserIdAsync(int userId);
}