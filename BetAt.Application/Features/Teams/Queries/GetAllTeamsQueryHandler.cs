namespace BetAt.Application.Features.Teams.Queries;

public class GetAllTeamsQueryHandler(ITeamRepository repository) : IRequestHandler<GetAllTeamsQuery, List<TeamDto>>
{
    public async Task<List<TeamDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        var teams = await repository.GetAllAsync(request.SearchTerm, request.Country);

        return teams.Select(t => new TeamDto
        {
            Id = t.Id,
            Name = t.Name,
            ShortName = t.ShortName,
            Country = t.Country,
            LogoUrl = t.LogoUrl,
        }).ToList();
    }
}