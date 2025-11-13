namespace BetAt.Domain.Entities;

public class League : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public int CreatedById { get; set; }
    public bool IsActive { get; set; } = true;
    
    
    public User CreatedBy { get; set; } = null!;
    public ICollection<LeagueMember> Members { get; set; } = [];
    public ICollection<Bet> Bets { get; set; } = [];
    public BetRule? BetRule { get; set; }
}