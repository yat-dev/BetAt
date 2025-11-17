namespace BetAt.Domain.Entities;

public class Venue
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string? ImageUrl { get; set; }
    public string? Country { get; set; }
    
    // Navigation
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}