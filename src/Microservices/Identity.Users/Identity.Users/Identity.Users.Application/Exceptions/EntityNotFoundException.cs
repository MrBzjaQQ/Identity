using System.Diagnostics.CodeAnalysis;

namespace Identity.Users.Application.Exceptions;
public class EntityNotFoundException: HttpException
{
    public EntityNotFoundException(string? message = null)
        : base(404, message)
    {
    }

    public static void ThrowIfNull([NotNull]object? value, string? message = null)
    {
        if (value == null)
        {
            throw new EntityNotFoundException(message);
        }
    }
    public static void ThrowIfEmpty<T>(IEnumerable<T> value, string? message = null)
    {
        if (!value.Any())
        {
            throw new EntityNotFoundException(message);
        }
    }
}
