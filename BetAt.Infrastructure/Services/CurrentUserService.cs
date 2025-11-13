namespace BetAt.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public int UserId
    {
        get
        {
            var claim = httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier);
                
            if (claim == null)
                return 0;
                
            return int.TryParse(claim.Value, out var userId) ? userId : 0;
        }
    }

    public string Username
    {
        get
        {
            var claim = httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.Name);
                
            return claim?.Value ?? string.Empty;
        }
    }
}