namespace BetAt.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            BetAt.Application.Common.Exceptions.NotFoundException notFoundEx => new ErrorResponse
            {
                Status = StatusCodes.Status404NotFound,
                Message = notFoundEx.Message
            },
            BetAt.Application.Common.Exceptions.BadRequestException badRequestEx => new ErrorResponse
            {
                Status = StatusCodes.Status400BadRequest,
                Message = badRequestEx.Message,
                Errors = badRequestEx.Errors
            },
            _ => new ErrorResponse
            {
                Status = StatusCodes.Status500InternalServerError,
                Message = "Une erreur s'est produite lors du traitement de votre requête."
            }
        };

        context.Response.StatusCode = response.Status;

        _logger.LogError(exception, "Exception interceptée: {Message}", exception.Message);

        await context.Response.WriteAsJsonAsync(response);
    }
}