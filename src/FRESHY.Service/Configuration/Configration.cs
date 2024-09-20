using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FRESHY.Authentication.Infrastructure;
using FRESHY.Main.Application;
using FRESHY.Main.Infrastructure;
using FRESHY.SharedKernel;
using FRESHY.Service.Config;
using Microsoft.AspNetCore.Hosting;
using FRESHY.Authentication.Application;

namespace FRESHY.Service.Configuration
{
    public static class Configration
    {
        public static void Configure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMappingConfigurations()

        //Authentication
        //    
        .AddFreshyAuthenticationInfrastructureServices(configuration)
        .AddFreshyAuthenticationApplicationServices()
        //Freshy
        // .AddFreshyApplicationServices()
        .AddFreshyInfrastructureServices(configuration)
        //Shared
        .AddShearedKernelServices(configuration);


        }
    }
}
