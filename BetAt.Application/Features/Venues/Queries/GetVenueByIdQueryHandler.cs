using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Venues.Queries;

public class GetVenueByIdQueryHandler(IVenueRepository repository) : IRequestHandler<GetVenueByIdQuery, VenueDto>
{
    public async Task<VenueDto> Handle(GetVenueByIdQuery request, CancellationToken cancellationToken)
    {
        var venue = await repository.GetByIdAsync(request.Id);

        return new VenueDto()
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