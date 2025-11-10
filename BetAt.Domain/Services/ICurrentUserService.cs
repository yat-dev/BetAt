namespace BetAt.Domain.Services;

public interface ICurrentUserService
{
    int UserId { get; }
    string Username { get; }
}