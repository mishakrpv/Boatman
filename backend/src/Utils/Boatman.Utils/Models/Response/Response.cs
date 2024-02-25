namespace Boatman.Utils.Models.Response;

public class Response
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = string.Empty;
    public IEnumerable<string>? Errors { get; set; }
}