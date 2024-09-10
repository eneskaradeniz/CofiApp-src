using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Maybe<User>> GetByEmailAsync(string email) =>
            await DbContext.Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

        public async Task<bool> IsEmailUniqueAsync(string email) =>
             !await DbContext.Set<User>()
                .AsNoTracking()
                .AsQueryable()
                .AnyAsync(x => x.Email == email);
    }
}
