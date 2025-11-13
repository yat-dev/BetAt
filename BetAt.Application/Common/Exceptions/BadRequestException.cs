namespace BetAt.Application.Common.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
    
    public IDictionary<string, string[]>? Errors { get; set; }

    public BadRequestException(string message, IDictionary<string, string[]> errors) 
        : base(message)
    {
        Errors = errors;
    }
}