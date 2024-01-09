namespace Identity.Users.Application.Exceptions;
public class BadRequestException: HttpException
{
    public BadRequestException(string? message = null)
        : base(400, message)
    {
    }
}
