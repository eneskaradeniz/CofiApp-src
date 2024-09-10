using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Infrastructure.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly IDbContext _dbContext;

        public PermissionService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
        {
            ICollection<Role>[] roles = await _dbContext.Set<User>()
                .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
                .Where(x => x.Id == userId)
                .Select(x => x.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(x => x)
                .SelectMany(x => x.Permissions)
                .Select(x => x.Name)
                .ToHashSet();
        }
    }
}
