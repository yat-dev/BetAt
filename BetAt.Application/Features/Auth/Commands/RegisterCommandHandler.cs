using BetAt.Domain.Entities;
using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.Auth.Commands;

public class RegisterCommandHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.Email, nameof(request.Email));
        ArgumentNullException.ThrowIfNull(request.Password, nameof(request.Password));

        if (await userRepository.ExistsAsync(request.Email))
            throw new Exception($"Email {request.Email} already exists");
        
        string password = authService.HashPassword(request.Password);
        
        User user = new User(request.Username, request.Email, password, request.DisplayName);
        
        await userRepository.AddAsync(user);
        
        string token = authService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Email = user.Email
        };
    }
}