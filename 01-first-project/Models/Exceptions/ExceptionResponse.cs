namespace _01_first_project.Models.Exceptions;

public class ExceptionResponse(string method, string path, string message)
{
    public string Method { get; set; } = method;
    public string Path { get; set; } = path;
    public string Message { get; set; } = message;
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}
