using BetAt.Application.Dtos.Venues;

namespace BetAt.Application.Features.Venues.Queries;

public class GetAllVenuesQueryHandler(IVenueRepository repository) : IRequestHandler<GetAllVenuesQuery, List<VenueDto>>
{
    public async Task<List<VenueDto>> Handle(GetAllVenuesQuery request, CancellationToken cancellationToken)
    {
        var venues = await repository.GetAllAsync(request.SearchTerm, request.Country);

        return venues.Select(v => new VenueDto
        {
            Id = v.Id,
            Name = v.Name,
            City = v.City,
            Capacity = v.Capacity,
            Country = v.Country,
            ImageUrl = v.ImageUrl
        }).ToList();
    }
}