namespace BetAt.Application.Mapping;

public static class LeagueMappingExtensions
{
    public static LeagueDto ToDto(this League league)
    {
        LeagueDto dto = new LeagueDto()
        {
            Id = league.Id,
            Name = league.Name,
            Code = league.Code,
            CreatedAt = league.CreatedAt,
            Description = league.Description,
            IsActive = league.IsActive,
            CreatedById = league.CreatedById
        };
        
        return dto;
    }
}