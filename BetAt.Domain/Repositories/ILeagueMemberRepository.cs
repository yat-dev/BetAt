namespace BetAt.Domain.Repositories;

public interface ILeagueMemberRepository
{
    Task<List<LeagueMember>> GetAllAsync();
    
    Task<int> GetCountByUserIdAsync(int userId);
    
    Task<int> GetAllPointsByUserIdAsync(int userId);
    
    Task<LeagueMember?> GetByUserAndLeagueAsync(int userId, int leagueId);
    
    Task<int> GetMembersCountAsync(int leagueId);
    
    Task<LeagueMember> AddAsync(LeagueMember leagueMember);
    
    Task DeleteAsync(LeagueMember leagueMember);
}