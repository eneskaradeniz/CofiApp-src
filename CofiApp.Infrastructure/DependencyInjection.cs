using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Common;
using CofiApp.Application.Abstractions.Cryptography;
using CofiApp.Application.Abstractions.Emails;
using CofiApp.Infrastructure.Authentication;
using CofiApp.Infrastructure.Authentication.Settings;
using CofiApp.Infrastructure.Common;
using CofiApp.Infrastructure.Cryptography;
using CofiApp.Infrastructure.Emails;
using CofiApp.Infrastructure.Emails.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CofiApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]))
                });

            services.AddAuthorization();

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            services.AddScoped<IPermissionService, PermissionService>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

            services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

            services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

            services.AddSingleton<JwtSecurityTokenHandler>();

            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddScoped<ITokenValidator, TokenValidator>();

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
