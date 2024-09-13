using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.UserRefreshTokens;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Maybe<UserRefreshToken>> GetByTokenAsync(string token, CancellationToken cancellationToken = default) =>
            await DbContext.Set<UserRefreshToken>()
                .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);

        public async Task<Maybe<UserRefreshToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
            await DbContext.Set<UserRefreshToken>()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}
