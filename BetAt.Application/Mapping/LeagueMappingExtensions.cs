namespace BetAt.Application.Mapping;

public static class LeagueMappingExtensions
{
    public static LeagueDto ToDto(this League league)
    {
        return new LeagueDto()
        {
            Id = league.Id,
            Name = league.Name,
            Code = league.Code,
            CreatedAt = league.CreatedAt,
            CreatedById = league.CreatedById,
            Description = league.Description,
            IsActive = league.IsActive
        };
    }
}