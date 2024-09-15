using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.BasketItemOptionGroups;
using CofiApp.Domain.BasketItemOptions;
using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Baskets;
using CofiApp.Domain.MenuCategories;
using CofiApp.Domain.OrderItemOptionGroups;
using CofiApp.Domain.OrderItemOptions;
using CofiApp.Domain.OrderItems;
using CofiApp.Domain.Orders;
using CofiApp.Domain.ProductMenuCategories;
using CofiApp.Domain.ProductOptionGroups;
using CofiApp.Domain.ProductOptions;
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

            services.AddScoped<IProductOptionGroupRepository, ProductOptionGroupRepository>();

            services.AddScoped<IProductOptionRepository, ProductOptionRepository>();

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped<IBasketItemRepository, BasketItemRepository>();

            services.AddScoped<IBasketItemOptionGroupRepository, BasketItemOptionGroupRepository>();

            services.AddScoped<IBasketItemOptionRepository, BasketItemOptionRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            services.AddScoped<IOrderItemOptionGroupRepository, OrderItemOptionGroupRepository>();

            services.AddScoped<IOrderItemOptionRepository, OrderItemOptionRepository>();

            return services;
        }
    }
}
