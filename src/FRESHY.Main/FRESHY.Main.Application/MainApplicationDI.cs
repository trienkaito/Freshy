using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FRESHY.Main.Application;

public static class MainApplicationDI
{
    public static IServiceCollection AddFreshyApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
                      config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}