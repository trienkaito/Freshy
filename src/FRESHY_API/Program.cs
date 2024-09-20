using FRESHY.Authentication.Infrastructure;
using FRESHY.Main.Application;
using FRESHY.Main.Infrastructure;
using FRESHY.SharedKernel;
using FRESHY_API.Config;

namespace FRESHY_API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json")
            .Build();

        builder.Services
        //Mapping
        .AddMappingConfigurations()
        //Authentication
        .AddFreshyAuthenticationInfrastructureServices(configuration)
        //Freshy
        .AddFreshyApplicationServices()
        .AddFreshyInfrastructureServices(configuration)
        //Shared
        .AddShearedKernelServices(configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        var app = builder.Build();
        
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}