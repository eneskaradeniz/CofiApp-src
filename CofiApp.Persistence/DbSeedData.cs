using CofiApp.Application.Abstractions.Common;
using CofiApp.Application.Abstractions.Cryptography;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.MenuCategories;
using CofiApp.Domain.ProductMenuCategories;
using CofiApp.Domain.Products;
using CofiApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence
{
    public class DbSeedData : IDbSeedData
    {
        private readonly IDateTime _dateTime;
        private readonly IPasswordHasher _passwordHasher;

        public DbSeedData(IPasswordHasher passwordHasher, IDateTime dateTime)
        {
            _passwordHasher = passwordHasher;
            _dateTime = dateTime;
        }

        public async Task RunAsync(ModelBuilder builder)
        {
            await CreateAdministrator(builder);
            await CreateProducts(builder);

            await Task.CompletedTask;
        }

        private async Task CreateAdministrator(ModelBuilder builder)
        {
            Guid roleId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            Role adminRole = new()
            {
                Id = roleId,
                Name = "Administrator",
                CreatedOnUtc = _dateTime.UtcNow
            };

            builder.Entity<Role>().HasData(adminRole);

            builder.Entity<RolePermission>().HasData(new RolePermission()
            {
                RoleId = roleId,
                PermissionId = (int)Domain.Enums.Permission.All
            });

            User adminUser = new()
            {
                Id = userId,
                FirstName = "Enes",
                LastName = "Karadeniz",
                Email = "eneskrdnz28@gmail.com",
                PasswordHash = _passwordHasher.Hash("123"),
                EmailConfirmed = true,
                CreatedOnUtc = _dateTime.UtcNow
            };

            builder.Entity<User>().HasData(adminUser);

            builder.Entity<UserRole>().HasData(new UserRole
            {
                RoleId = roleId,
                UserId = userId
            });

            await Task.CompletedTask;
        }

        private async Task CreateProducts(ModelBuilder builder)
        {
            // 3 kategori oluştur (Öne Çıkaranlar, Sıcak Kahveler, Soğuk Kahveler)

            MenuCategory oneCikaranlar = new()
            {
                Id = Guid.NewGuid(),
                Name = "Öne Çıkaranlar",
                CreatedOnUtc = _dateTime.UtcNow
            };

            MenuCategory sicakKahveler = new()
            {
                Id = Guid.NewGuid(),
                Name = "Sıcak Kahveler",
                CreatedOnUtc = _dateTime.UtcNow
            };

            MenuCategory sogukKahveler = new()
            {
                Id = Guid.NewGuid(),
                Name = "Soğuk Kahveler",
                CreatedOnUtc = _dateTime.UtcNow
            };

            builder.Entity<MenuCategory>().HasData(oneCikaranlar, sicakKahveler, sogukKahveler);

            // 3 ürün oluştur (Latte, Mocha, Americano)

            Product latte = new()
            {
                Id = Guid.NewGuid(),
                Name = "Latte",
                Price = 15,
                CreatedOnUtc = _dateTime.UtcNow
            };

            Product mocha = new()
            {
                Id = Guid.NewGuid(),
                Name = "Mocha",
                Price = 20,
                CreatedOnUtc = _dateTime.UtcNow
            };

            Product iceAmericano = new()
            {
                Id = Guid.NewGuid(),
                Name = "Ice Americano",
                Price = 10,
                CreatedOnUtc = _dateTime.UtcNow
            };

            Product iceLatte = new()
            {
                Id = Guid.NewGuid(),
                Name = "Ice Latte",
                Price = 10,
                CreatedOnUtc = _dateTime.UtcNow
            };

            builder.Entity<Product>().HasData(latte, mocha, iceAmericano, iceLatte);

            // 3 ürünü 3 kategoriye de ekle (latte ve mocha öne çıkaranlar ve sıcak kahveler kategorisine, ice americano soğuk kahveler kategorisine)

            ProductMenuCategory productMenuCategory1 = new()
            {
                ProductId = latte.Id,
                MenuCategoryId = oneCikaranlar.Id
            };

            ProductMenuCategory productMenuCategory2 = new()
            {
                ProductId = latte.Id,
                MenuCategoryId = sicakKahveler.Id
            };

            ProductMenuCategory productMenuCategory3 = new()
            {
                ProductId = mocha.Id,
                MenuCategoryId = oneCikaranlar.Id
            };

            ProductMenuCategory productMenuCategory4 = new()
            {
                ProductId = mocha.Id,
                MenuCategoryId = sicakKahveler.Id
            };

            ProductMenuCategory productMenuCategory5 = new()
            {
                ProductId = iceAmericano.Id,
                MenuCategoryId = sogukKahveler.Id
            };

            ProductMenuCategory productMenuCategory6 = new()
            {
                ProductId = iceLatte.Id,
                MenuCategoryId = sogukKahveler.Id
            };

            builder.Entity<ProductMenuCategory>().HasData(productMenuCategory1, productMenuCategory2, productMenuCategory3, productMenuCategory4, productMenuCategory5, productMenuCategory6);

            await Task.CompletedTask;
        }
    }
}
