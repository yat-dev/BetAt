namespace BetAt.Domain.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    
    Task<User?> GetByIdAsync(int userId);
}