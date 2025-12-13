namespace BetAt.Application.Features.Teams.Commands;

public class CreateTeamCommandHandler(ITeamRepository repository) : IRequestHandler<CreateTeamCommand, TeamDto>
{
    public async Task<TeamDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        Team team = new Team
        {
            Name = request.Dto.Name,
            ShortName = request.Dto.ShortName,
            LogoUrl = request.Dto.LogoUrl,
            Country = request.Dto.Country,
            ExternalApiId = request.Dto.ExternalApiId
        };

        await repository.CreateAsync(team);

        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            ShortName = team.ShortName,
            LogoUrl = team.LogoUrl,
            Country = team.Country,
        };
    }
}