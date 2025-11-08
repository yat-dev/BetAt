namespace TodoApp.Application.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        if (await _unitOfWork.Users.ExistsAsync(request.Email))
            throw new Exception($"Email {request.Email} already exists");
        
        string passwordHash = _authService.HashPassword(request.Password);
        User user = new User(request.Username, request.Email, passwordHash);
        
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        string token = _authService.GenerateToken(user);

        return new AuthResponseDto()
        {
            Token = token,
            Username = user.Username,
            Email = user.Email
        };
    }
}