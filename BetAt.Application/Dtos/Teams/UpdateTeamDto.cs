namespace BetAt.Application.Dtos.Teams;

public class UpdateTeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Country { get; set; } = string.Empty;
}