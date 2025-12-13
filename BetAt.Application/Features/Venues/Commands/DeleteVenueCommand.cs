namespace BetAt.Application.Features.Venues.Commands;

public class DeleteVenueCommand : IRequest
{
    public int Id { get; set; }
}