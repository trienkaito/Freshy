using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Infrastructure.Persistance;
using FRESHY.Main.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FRESHY.Main.Infrastructure;

public static class MainInfrastructureDI
{
    public static IServiceCollection AddFreshyInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository()
        .AddPersistanceServices(configuration)
        .AddSingleton(configuration);

        return services;
    }

    private static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FreshyDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("FRESHY_DB"));
        });

        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<ICustomerProfileRepository, CustomerProfileRepository>();
        services.AddScoped<IEmployeeProfileRepository, EmployeeProfileRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IShippingRepository, ShippingRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IJobPositionRepository, JobPositionRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderAddressRepository, OrderAddressRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IProductLikerepository, ProductLikeRepository>();
        services.AddScoped<IProductUnitRepository, ProductUnitRepository>();

        return services;
    }
}