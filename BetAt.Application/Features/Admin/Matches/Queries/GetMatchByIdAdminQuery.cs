using BetAt.Application.Dtos.Matches;

namespace BetAt.Application.Features.Admin.Matches.Queries;

public class GetMatchByIdAdminQuery : IRequest<MatchDto>
{
    public int Id { get; set; }
}