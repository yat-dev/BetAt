namespace BetAt.Domain.Repositories;

public interface ILeagueRepository
{
    Task<List<League>> GetAllLeaguesAsync();
    Task<League?> GetLeagueByIdAsync(int id);
    Task<League?> GetLeagueByNameAsync(string name);
    Task<League?> GetLeagueByCodeAsync(string code);
}