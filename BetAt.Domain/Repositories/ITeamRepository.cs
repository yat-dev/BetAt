namespace BetAt.Domain.Repositories;

public interface ITeamRepository
{
    Task<List<Team>> GetAllAsync();

    Task<List<Team>> GetAllAsync(string? searchTerm, string? country);

    Task<Team?> GetByIdAsync(int id);
    
    Task CreateAsync(Team team);
    
    Task UpdateAsync(Team team);
    
    Task DeleteAsync(Team team);
    
    Task<bool> HasMatchAsync(Team team);
}