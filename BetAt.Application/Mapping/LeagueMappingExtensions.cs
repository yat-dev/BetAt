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
            Description = league.Description,
            IsActive = league.IsActive,
            CreatedBy = new User
            {
                Username = league.CreatedBy.Username,
                Email = league.CreatedBy.Email,
                DisplayName = league.CreatedBy.DisplayName,
                LastLoginAt = league.CreatedBy.LastLoginAt,
            }
        };
    }
}