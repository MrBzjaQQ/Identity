using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Users.Application.Services.Users;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Users.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserManagerProxy, UserManagerProxy>();
        serviceCollection.AddTransient<IUsersService, UsersService>();
        return serviceCollection;
    }

    public static IServiceCollection AddValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>()
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        return serviceCollection;
    }
}
