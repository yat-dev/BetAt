using BetAt.Application.Dtos.Matches;

namespace BetAt.Application.Features.Admin.Matches.Commands;

public class DeleteMatchCommand : IRequest<bool>
{
    public int Id { get; set; }
}