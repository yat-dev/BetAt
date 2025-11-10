using BetAt.Application.Dtos;
using BetAt.Application.Features.LeagueMembers.Queries;

namespace BetAt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LeagueMemberController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LeagueMemberDto>>> GetAllAsync()
    {
        var response = await mediator.Send(new GetAllLeagueMembersQuery());
        
        return Ok(response);
    }

    [HttpGet("userleagues")]
    public async Task<ActionResult<int>> GetAllByUserAsync()
    {
        return await mediator.Send(new GetAllLeaguesMemberByUserIdQuery());
    }

    [HttpGet("points")]
    public async Task<ActionResult<int>> GetAllPointsByUserAsync()
    {
        return await mediator.Send(new GetAllLeagueMemberPointsByUserIdQuery());
    }
}