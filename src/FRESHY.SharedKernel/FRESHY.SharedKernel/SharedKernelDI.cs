using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.SharedKernel.Interfaces;
using FRESHY.SharedKernel.Persistance;
using FRESHY.SharedKernel.Persistance.Repositories;
using FRESHY.SharedKernel.Services;
using FRESHY.SharedKernel.Services.Events;
using FRESHY.SharedKernel.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FRESHY.SharedKernel;

public static class SharedKernelDI
{
    public static IServiceCollection AddShearedKernelServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<EventInterceptor>()
        .AddScoped<IEventStore, EventStore>()
        .AddScoped<IQrService, QrService>()
        .AddCloudinaryService(configuration)
        .AddSingleton(provider => new EventSourcingDbContext(configuration));

        return services;
    }

    private static IServiceCollection AddCloudinaryService(this IServiceCollection services, IConfiguration configuration)
    {
        var cloudinarySettings = new CloudinarySettings();
        configuration.Bind(cloudinarySettings.SectionName);
        services.AddSingleton(cloudinarySettings);
        services.Configure<CloudinarySettings>(configuration.GetSection(cloudinarySettings.SectionName));

        services.AddScoped<IImageService, ImageService>();

        return services;
    }
}