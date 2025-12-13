namespace BetAt.Application.Features.Teams.Commands;

public class UpdateTeamCommandHandler(ITeamRepository repository) : IRequestHandler<UpdateTeamCommand, TeamDto>
{
    public async Task<TeamDto> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await repository.GetByIdAsync(request.Dto.Id);
        
        if (team == null)
            throw new Exception($"Team {request.Dto.Id} not found.");
        
        await repository.UpdateAsync(team);

        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            Country = team.Country,
            LogoUrl = team.LogoUrl,
            ShortName = team.ShortName
        };
    }
}