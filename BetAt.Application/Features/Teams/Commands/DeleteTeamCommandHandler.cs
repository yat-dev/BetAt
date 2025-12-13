namespace BetAt.Application.Features.Teams.Commands;

public class DeleteTeamCommandHandler(ITeamRepository repository) : IRequestHandler<DeleteTeamCommand>
{
    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await repository.GetByIdAsync(request.Id);
        
        if (team == null)
            throw new NotFoundException($"Team {request.Id} not found");
        
        if (await repository.HasMatchAsync(team))
            throw new Exception("Cette équipe est utilisée par des matchs existants. Suppression impossible."); 
        
        await repository.DeleteAsync(team);
    }
}