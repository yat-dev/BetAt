namespace BetAt.Application.Dtos.Leagues;

public class LeagueDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public User CreatedBy { get; set; } = null!;
}