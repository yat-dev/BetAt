using BetAt.Application.Dtos.Matches;

namespace BetAt.Application.Features.Admin.Matches.Commands;

public class UpdateMatchCommand : IRequest<MatchDto>
{
    public UpdateMatchDto Dto { get; set; } = null!;
}