namespace BetAt.Application.Features.Teams.Queries;

public class GetTeamByIdQueryHandler(ITeamRepository repository) : IRequestHandler<GetTeamByIdQuery, TeamDto>
{
    public async Task<TeamDto> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await repository.GetByIdAsync(request.Id);
        
        if (team == null)
            throw new NotFoundException($"Team {request.Id} was not found.");
        
        return new TeamDto
        {
            Id = team.Id,
            LogoUrl = team.LogoUrl,
            Name = team.Name,
            Country = team.Country,
            ShortName = team.ShortName
        };
    }
}