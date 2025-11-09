namespace Infrastructure.UnitTests.User;

public class GetAllUsersTests
{
    private readonly UserRepository _repository;

    public GetAllUsersTests(UserRepository repository)
    {
        _repository = repository;
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnAllUsers()
    {
        var user1 = new BetAt.Domain.Entities.User()
        {
            Id = 1,
            CreatedAt = DateTime.Now,
            Email = "test@test.com",
            PasswordHash = "test",
            DisplayName = "test",
            LastLoginAt = DateTime.Now
        };

        var user2 = new BetAt.Domain.Entities.User()
        {
            Id = 2,
            CreatedAt = DateTime.Now,
            Email = "test2@test.com",
            PasswordHash = "test2",
            DisplayName = "test2",
            LastLoginAt = DateTime.Now
        };

        var result = await _repository.GetAllAsync();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
}