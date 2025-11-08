namespace BetAt.Domain.Entities;

public class LeagueMember : BaseEntity
{
    public int LeagueId { get; set; }
    
    public int UserId { get; set; }
    
    public DateTime JoinedAt { get; set; }
    
    public Roles Role { get; set; }
    
    public int Points { get; set; }
}