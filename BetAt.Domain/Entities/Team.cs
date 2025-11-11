namespace BetAt.Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string? LogoUrl { get; set; }
    public string? Country { get; set; }
    public int? ExternalApiId { get; set; }
    
    public ICollection<Match> HomeMatches { get; set; }
    public ICollection<Match> AwayMatches { get; set; }
}