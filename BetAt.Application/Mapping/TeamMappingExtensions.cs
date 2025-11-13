namespace BetAt.Application.Mapping;

public static class TeamMappingExtensions
{
    public static TeamDto ToDto(this Team team)
    {
        return new TeamDto()
        {
            Id = team.Id,
            Name = team.Name,
            ShortName = team.ShortName,
            Country = team.Country,
            LogoUrl = team.LogoUrl
        };
    }
}