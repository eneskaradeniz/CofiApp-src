using Azure.Storage.Blobs;
using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Caching;
using CofiApp.Application.Abstractions.Common;
using CofiApp.Application.Abstractions.Cryptography;
using CofiApp.Application.Abstractions.Emails;
using CofiApp.Application.Abstractions.EventBus;
using CofiApp.Application.Abstractions.Notifications;
using CofiApp.Application.Abstractions.Storage;
using CofiApp.Application.Orders.Commands.ProcessShopOrder;
using CofiApp.Infrastructure.Authentication;
using CofiApp.Infrastructure.Authentication.Settings;
using CofiApp.Infrastructure.Caching;
using CofiApp.Infrastructure.Common;
using CofiApp.Infrastructure.Cryptography;
using CofiApp.Infrastructure.Emails;
using CofiApp.Infrastructure.Emails.Settings;
using CofiApp.Infrastructure.Messaging;
using CofiApp.Infrastructure.Messaging.Settings;
using CofiApp.Infrastructure.Notifications.HubService;
using CofiApp.Infrastructure.Storage;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CofiApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Cache");
            });

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

            services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.SettingsKey));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                // Add consumers
                busConfigurator.AddConsumer<OrderProcessingEventConsumer>();

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    var settings = context.GetRequiredService<MessageBrokerSettings>();

                    configurator.Host(new Uri(settings.HostName), h =>
                    {
                        h.Username(settings.UserName);
                        h.Password(settings.Password);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

            services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

            services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

            services.AddSingleton<JwtSecurityTokenHandler>();

            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddScoped<ITokenValidator, TokenValidator>();

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IEventBus, EventBus>();

            services.AddTransient<ICacheService, CacheService>();

            services.AddTransient<IOrderHubService, OrderHubService>();

            services.AddSignalR();

            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));

            return services;
        }
    }
}
