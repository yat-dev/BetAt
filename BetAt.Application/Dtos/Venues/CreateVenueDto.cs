namespace BetAt.Application.Dtos.Venues;

public class CreateVenueDto
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}