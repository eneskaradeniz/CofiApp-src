using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.MenuCategories;
using CofiApp.Domain.ProductMenuCategories;
using CofiApp.Domain.Products;
using CofiApp.Domain.UserRefreshTokens;
using CofiApp.Domain.Users;
using CofiApp.Domain.UserVerificationTokens;
using CofiApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CofiApp.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
                connectionString = string.Format(connectionString, password);
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IDbContext>(serviceProdiver =>
                serviceProdiver.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(serviceProdiver =>
                serviceProdiver.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IDbSeedData, DbSeedData>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();

            services.AddScoped<IUserVerificationTokenRepository, UserVerificationTokenRepository>();

            services.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductMenuCategoryRepository, ProductMenuCategoryRepository>();

            return services;
        }
    }
}
