namespace BetAt.Domain.Repositories;

public interface IBetRepository
{
    Task<List<Bet>> GetAllAsync();
    
    Task<List<Bet>> GetAllByUserIdAsync(int userId);
    
    Task<List<Bet>> GetAllByLeagueIdAndStatusAsync(int userId, int? leagueId, BetStatus status);

    Task<List<Bet>> GetAllByMatchIdAsync(int matchId);
    
    Task<Bet?> GetByIdAsync(int id, int userId);
    
    Task<Bet> AddAsync(Bet bet);
    
    Task UpdateAsync(Bet bet);
        
    Task DeleteAsync(Bet bet);
}