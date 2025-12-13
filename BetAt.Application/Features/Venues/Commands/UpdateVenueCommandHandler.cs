using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Venues.Commands;

public class UpdateVenueCommandHandler(IVenueRepository repository) : IRequestHandler<UpdateVenueCommand, VenueDto>
{
    public async Task<VenueDto> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
    {
        Venue venue = new Venue()
        {
            Id = request.Dto.Id,
            Name = request.Dto.Name,
            Capacity = request.Dto.Capacity,
            City = request.Dto.City,
            Country = request.Dto.Country,
            ImageUrl = request.Dto.ImageUrl
        };
        
        await repository.UpdateAsync(venue);

        return new VenueDto
        {
            Id = venue.Id,
            Name = venue.Name,
            Capacity = venue.Capacity,
            City = venue.City,
            Country = venue.Country,
            ImageUrl = venue.ImageUrl
        };
    }
}