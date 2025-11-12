namespace BetAt.Domain.Repositories;

public interface IMatchRepository
{
    Task<List<Match>> GetAllAsync();
    
    Task<List<Match>> GetAllUpcomingAsync(int days);
}