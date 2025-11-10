namespace BetAt.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public DateTime? LastLoginAt { get; set; }
    
    public ICollection<League> CreatedLeagues { get; set; } = [];
    public ICollection<LeagueMember> LeagueMemberships { get; set; } = [];
    public ICollection<Bet> Bets { get; set; } = [];
    public ICollection<Notification> Notifications { get; set; } = [];

    public User()
    {
    }
    
    public User(string username, string email, string passwordHash, string displayName)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        CreatedAt = DateTime.UtcNow;
        DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
    }
}