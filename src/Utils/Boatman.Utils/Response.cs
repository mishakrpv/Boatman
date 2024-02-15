namespace Boatman.Utils;

public class Response
{
    public int StatusCode { get; init; } = 200;
    public string Message { get; init; } = string.Empty;
    public IEnumerable<string>? Errors { get; init; }
}