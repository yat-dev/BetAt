namespace BetAt.Domain.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    
    Task<User?> GetByIdAsync(int userId);
    
    Task<User?> GetByEmailAsync(string email);
    
    Task<bool> ExistsAsync(string email);
    
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
}