namespace BetAt.Application.Features.Admin.Matches.Commands;

public class DeleteMatchCommandHandler(IMatchRepository matchRepository, IBetRepository betRepository) : IRequestHandler<DeleteMatchCommand, bool>
{
    public async Task<bool> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await matchRepository.GetByIdAsync(request.Id);
        
        if (match == null)
            throw new NotFoundException($"Match to delete {request.Id} not found");
        
        var hasBets = await betRepository.IsMatchHasBetAsync(match.Id);
        
        if (hasBets)
            throw new Exception("Ce match a des paris existants. Suppression impossible.");
        
        return await matchRepository.DeleteAsync(match);
    }
}