using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FRESHY.Service.Config;

public static class MappingDI
{
    public static IServiceCollection AddMappingConfigurations(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(implementationInstance: config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}