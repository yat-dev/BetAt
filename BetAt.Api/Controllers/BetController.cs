using BetAt.Application.Features.Bet.Commands;

namespace BetAt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BetController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BetDto>>> Get()
    {
        var bets = await mediator.Send(new GetAllBetsByUserIdQuery());
        
        return Ok(bets);
    }

    [HttpGet("{matchId}")]
    public async Task<ActionResult<BetDto>> GetBetByMatchIdAndUser(int matchId)
    {
        var bet = await mediator.Send(new GetBetByMatchIdAndUserQuery { MatchId = matchId });
        
        if (bet == null)
            return NotFound($"Aucun pari pour le match {matchId} n'a été trouvé");
        
        return Ok(bet);
    }
    
    [HttpPost]
    public async Task<ActionResult<BetDto>> PlaceBet([FromBody] CreateBetCommand command)
    {
        var bet = await mediator.Send(command);
        
        return Ok(bet);
    }
}