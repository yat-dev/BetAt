namespace BetAt.Domain.Repositories;

public interface IVenueRepository
{
    Task<List<Venue>> GetAllAsync();
    
    Task<List<Venue>> GetAllAsync(string? searchTerm, string? country);
    
    Task<Venue> GetByIdAsync(int id);
    
    Task CreateAsync(Venue venue);
    
    Task UpdateAsync(Venue venue);
    
    Task DeleteAsync(Venue venue);
    
    Task<bool> HasMatchAsync(Venue venue);
}