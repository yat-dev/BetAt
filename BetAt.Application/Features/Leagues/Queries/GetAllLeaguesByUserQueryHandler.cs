namespace BetAt.Application.Features.Leagues.Queries;

public class GetAllLeaguesByUserQueryHandler(ILeagueRepository repository, ICurrentUserService userService) : IRequestHandler<GetAllLeaguesByUserQuery, List<LeagueDto>>
{
    public async Task<List<LeagueDto>> Handle(GetAllLeaguesByUserQuery request, CancellationToken cancellationToken)
    {
        var leagues = await repository.GetAllByUserIdAsync(userService.UserId);

        return leagues.Select(l => new LeagueDto()
        {
            Id = l.Id,
            Name = l.Name,
            Code = l.Code,
            Description = l.Description,
            CreatedById = l.CreatedById,
            IsActive = l.IsActive,
            CreatedAt = l.CreatedAt
        }).ToList();
    }
}