namespace Identity.Users.Application.Exceptions;
public abstract class HttpException: Exception
{
    public int StatusCode { get; init; }

    public HttpException(int statusCode, string? message = null): base(message)
    {
        StatusCode = statusCode;
    }
}
