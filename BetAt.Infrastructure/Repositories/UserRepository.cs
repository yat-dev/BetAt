using BetAt.Domain.Repositories;

namespace BetAt.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private BetAtDbContext _context;

    public UserRepository(BetAtDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }
}