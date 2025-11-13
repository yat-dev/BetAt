namespace BetAt.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key) 
        : base($"L'entité '{name}' avec la clé ({key}) n'a pas été trouvée.")
    {
    }
}