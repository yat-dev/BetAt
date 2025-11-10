namespace BetAt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsersAsync()
    {
        var result = await mediator.Send(new GetAllUsersQuery());
        
        return Ok(result);
    }
}