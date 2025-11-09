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
}