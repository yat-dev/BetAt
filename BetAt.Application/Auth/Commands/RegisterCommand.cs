using MediatR;

namespace TodoApp.Application.Auth.Commands;

public class RegisterCommand : IRequest<AuthResponseDto>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthResponseDto
{
    public string Token { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

