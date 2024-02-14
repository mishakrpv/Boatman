namespace Boatman.Utils;

public class Response<T> : Response
{
    public Response()
    {
    }

    public Response(T value)
    {
        Value = value;
    }

    public T? Value { get; init; }
}