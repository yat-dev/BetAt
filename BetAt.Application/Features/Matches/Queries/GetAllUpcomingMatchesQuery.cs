using BetAt.Application.Dtos.Matches;

namespace BetAt.Application.Features.Matches.Queries;

public class GetAllUpcomingMatchesQuery : IRequest<List<MatchDto>>
{
    public int Days { get; set; }
}