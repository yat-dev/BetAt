using BetAt.Application.Dtos.Matches;

namespace BetAt.Application.Features.Admin.Matches.Commands;

public class CreateMatchCommand : IRequest<MatchDto>
{
    public CreateMatchDto Dto { get; set; } = null!;
}