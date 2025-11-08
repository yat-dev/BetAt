using MediatR;

namespace TodoApp.Application.Auth.Commands;

public class LoginCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}