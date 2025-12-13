namespace BetAt.Infrastructure.Repositories;

public class VenueRepository(BetAtDbContext context) : IVenueRepository
{
    public async Task<List<Venue>> GetAllAsync()
    {
        return await context.Venue.ToListAsync();
    }

    public async Task<List<Venue>> GetAllAsync(string? searchTerm, string? country)
    {
        var query = context.Venue.AsQueryable();
        
        if (string.IsNullOrWhiteSpace(searchTerm) == false)
        {
            query = query.Where(v => 
                v.Name.Contains(searchTerm) || 
                v.City.Contains(searchTerm));
        }

        if (string.IsNullOrWhiteSpace(country) == false)
        {
            query = query.Where(v => v.Country == country);
        }
        
        return await query.OrderBy(v => v.Name).ToListAsync();
    }

    public async Task<Venue> GetByIdAsync(int id)
    {
        return await context.Venue.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task CreateAsync(Venue venue)
    {
        await context.Venue.AddAsync(venue);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Venue venue)
    {
        context.Venue.Update(venue);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Venue venue)
    {
        context.Venue.Remove(venue);
        await context.SaveChangesAsync();
    }

    public async Task<bool> HasMatchAsync(Venue venue)
    {
        return await context.Matches.AnyAsync(m => m.VenueId == venue.Id);
    }
}