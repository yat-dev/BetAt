namespace BetAt.Domain.Entities;

public class Notification : BaseEntity
{
    public int UserId { get; set; }
    
    public string Message { get; set; } = string.Empty;
    
    public NotificationTypes Type { get; set; }
    
    public bool IsRead { get; set; }
    
    public DateTime CreatedAt { get; set; }
}