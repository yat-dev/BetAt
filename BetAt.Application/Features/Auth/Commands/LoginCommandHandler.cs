using BetAt.Domain.Repositories;

namespace BetAt.Application.Features.Auth.Commands;

public class LoginCommandHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<LoginCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.Email, nameof(request.Email));
        ArgumentNullException.ThrowIfNull(request.Password, nameof(request.Password));
        
        var user = await userRepository.GetByEmailAsync(request.Email);
        
        if (user == null || authService.VerifyPassword(request.Password, user.PasswordHash) == false)
            throw new UnauthorizedAccessException("Invalid credentials");

        string token = authService.GenerateToken(user);
        
        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Username = user.Username
        };
    }
}