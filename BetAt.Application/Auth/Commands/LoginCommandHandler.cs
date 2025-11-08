using MediatR;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Application.Auth.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }
    
    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.Email, nameof(request.Email));
        
        User user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

        if (user == null || _authService.VerifyPassword(request.Password, user.PasswordHash) == false)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }
        
        string token = _authService.GenerateToken(user);

        return new AuthResponseDto()
        {
            Token = token,
            Email = user.Email,
            Username = user.Username
        };
    }
}