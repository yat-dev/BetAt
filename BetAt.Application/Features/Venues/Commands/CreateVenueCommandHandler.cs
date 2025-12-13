using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Venues.Commands;

public class CreateVenueCommandHandler(IVenueRepository repository) : IRequestHandler<CreateVenueCommand, VenueDto>
{
    public async Task<VenueDto> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
    {
        Venue venue = new Venue
        {
            Name = request.Dto.Name,
            Capacity = request.Dto.Capacity,
            City = request.Dto.City,
            Country = request.Dto.Country,
            ImageUrl = request.Dto.ImageUrl
        };

        await repository.CreateAsync(venue);
        
        return new VenueDto
        {
            Id = venue.Id,
            Name = venue.Name,
            City = venue.City,
            Country = venue.Country,
            Capacity = venue.Capacity,
            ImageUrl = venue.ImageUrl
        };
    }
}