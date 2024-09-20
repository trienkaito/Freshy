using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Share;
using FRESHY.Authentication.Application.Abtractions.UserAbtracttions.Validattion;
using FRESHY.Authentication.Application.Interfaces;
using FRESHY.Authentication.Application.Interfaces.Persistance;
using FRESHY.Authentication.Infrastructure.Implementations;
using FRESHY.Authentication.Infrastructure.Persistance;
using FRESHY.Authentication.Infrastructure.Persistance.Repositories;
using FRESHY.Authentication.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FRESHY.Authentication.Infrastructure;

public static class AuthenticationInfrastructureDI
{
    public static IServiceCollection AddFreshyAuthenticationInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FreshyAuthDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("FRESHY_AUTH"));
        });

        services.AddUserIdentityConfig()
        .AddRepositories()
        .AddAuthenticationServices(configuration);

        services.AddSingleton(configuration);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IValidation, Validation>();

        return services;
    }

    

    private static IServiceCollection AddUserIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;

            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<FreshyAuthDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        var jwtSettings = new JwtSettings();
        configuration.Bind(jwtSettings.SectionName, jwtSettings);
        services.AddSingleton(jwtSettings);
        services.Configure<JwtSettings>(configuration.GetSection(jwtSettings.SectionName));

        var googleAuthenticationSettings = new GoogleAuthenticationSettings();
        configuration.Bind(googleAuthenticationSettings.SectionName, googleAuthenticationSettings);
        services.AddSingleton(googleAuthenticationSettings);
        services.Configure<GoogleAuthenticationSettings>(configuration.GetSection(googleAuthenticationSettings.SectionName));

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey
                    (
                        Encoding.UTF8.GetBytes(jwtSettings.Secret!)
                    )
            })
            .AddGoogle(options =>
            {
                options.ClientId = googleAuthenticationSettings.ClientId;
                options.ClientSecret = googleAuthenticationSettings.ClientSecret;
            });

        return services;
    }
}