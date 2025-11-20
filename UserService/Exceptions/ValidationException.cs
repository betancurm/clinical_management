namespace UserService.Exceptions;

public class ValidationException : Exception
{
    public List<string> ValidationErrors { get; }

    public ValidationException(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }

    public ValidationException(string message, List<string> errors) : base(message)
    {
        ValidationErrors = errors;
    }
}