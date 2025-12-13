namespace BetAt.Application.Dtos.Teams;

public class CreateTeamDto
{
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Country { get; set; } = string.Empty;
    public int? ExternalApiId { get; set; }
}