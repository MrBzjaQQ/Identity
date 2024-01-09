using Identity.Users.Application.Exceptions;

namespace Identity.Users.Web.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpException ex)
        {
            await HandleExceptionAsync(context, ex);
            await _next(context);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, HttpException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsync(exception.Message);
    }
}
