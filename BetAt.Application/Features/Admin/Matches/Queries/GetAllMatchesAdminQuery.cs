using BetAt.Application.Dtos.Matches;

namespace BetAt.Application.Features.Admin.Matches.Queries;

public class GetAllMatchesAdminQuery : IRequest<List<MatchDto>>
{
    public string? Competition { get; set; }
    public MatchStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}