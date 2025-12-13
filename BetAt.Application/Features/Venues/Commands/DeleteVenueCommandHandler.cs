namespace BetAt.Application.Features.Venues.Commands;

public class DeleteVenueCommandHandler(IVenueRepository repository) : IRequestHandler<DeleteVenueCommand>
{
    public async Task Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
    {
        Venue venue = await repository.GetByIdAsync(request.Id);
        
        if (venue == null)
            throw new NotFoundException($"Venue {request.Id} not found.");

        var hasMatches = await repository.HasMatchAsync(venue);

        if (hasMatches)
        {
            throw new Exception($"Le stade {request.Id} est utilisé par des matchs existants. Suppression impossible.");
        }
        
        await repository.DeleteAsync(venue);
    }
}