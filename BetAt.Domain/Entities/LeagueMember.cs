namespace BetAt.Domain.Entities;

public class LeagueMember : BaseEntity
{
    public int LeagueId { get; set; }
    public int UserId { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public MemberRole Role { get; set; } = MemberRole.Member;
    public int Points { get; set; }
    
    public League League { get; set; } = null!;
    public User User { get; set; } = null!;
}