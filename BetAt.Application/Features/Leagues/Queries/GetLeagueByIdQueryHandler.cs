using BetAt.Domain.Entities;
using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.Leagues.Queries;

public class GetLeagueByIdQueryHandler(ILeagueRepository repository) : IRequestHandler<GetLeagueByIdQuery, LeagueDto?>
{
    public async Task<LeagueDto?> Handle(GetLeagueByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetLeagueByIdAsync(request.Id);

        if (result == null)
        {
            return null;
        }

        return new LeagueDto()
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            Code = result.Code,
            CreatedById = result.CreatedById,
            IsActive = result.IsActive
        };
    }
}