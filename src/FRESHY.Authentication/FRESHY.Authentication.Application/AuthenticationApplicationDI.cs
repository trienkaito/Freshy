using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Authentication.Application
{
    public static class AuthenticationApplicationDI
    {
        public static IServiceCollection AddFreshyAuthenticationApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                          config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
