using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default) =>
             !await DbContext.Set<Role>()
                .AsNoTracking()
                .AsQueryable()
                .AnyAsync(x => x.Name == name, cancellationToken);
    }
}
