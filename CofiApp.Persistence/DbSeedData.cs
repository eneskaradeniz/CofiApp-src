using CofiApp.Application.Abstractions.Common;
using CofiApp.Application.Abstractions.Cryptography;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Authentication;
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
    }
}
