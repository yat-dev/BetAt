using BetAt.Application.Dtos.BetRules;

namespace BetAt.Application.Dtos.Leagues;

public class LeagueDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public int CreatedById { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsUserAdmin { get; set; } // ← Ajouter cette propriété
    public List<LeagueMemberDto> Members { get; set; } = [];
    public BetRuleDto? BetRule { get; set; }
}