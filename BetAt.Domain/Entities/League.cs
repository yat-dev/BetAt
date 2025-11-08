namespace BetAt.Domain.Entities;

public class League : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Code { get; set; } = string.Empty;
    
    public int CreatedById { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public bool IsActive { get; set; }
}