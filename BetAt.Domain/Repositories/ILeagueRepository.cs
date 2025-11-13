namespace BetAt.Domain.Repositories;

public interface ILeagueRepository
{
    Task<List<League>> GetAllLeaguesAsync();
    
    Task<List<League>> GetMyLeaguesAsync(int userId);
    
    Task<League?> GetLeagueByIdAsync(int id);
    
    Task<League?> GetLeagueByNameAsync(string name);
    
    Task<League?> GetLeagueByCodeAsync(string code);
    
    Task<bool> CodeExistsAsync(string code);
    
    Task<League> AddAsync(League entity);
}