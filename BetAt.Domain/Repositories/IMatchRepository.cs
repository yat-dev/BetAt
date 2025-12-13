namespace BetAt.Domain.Repositories;

public interface IMatchRepository
{
    Task<List<Match>> GetAllAsync();
    
    Task<List<Match>> GetAllUpcomingAsync(int days);
    
    Task<List<Match>> GetAllLivesAsync();
    
    Task<List<Match>> GetAllFinishedMatchesAsync();
    
    Task<Match> GetByIdAsync(int id);
    
    Task<bool> IsMatchExistsAsync(int homeTeamId, int awayTeamId, DateTime matchDate);
    
    Task<Match> AddAsync(Match match);

    Task UpdateAsync(Match match);
    
    Task<bool> DeleteAsync(Match match);
}