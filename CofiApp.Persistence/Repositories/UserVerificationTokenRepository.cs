using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.UserVerificationTokens;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class UserVerificationTokenRepository :
        GenericRepository<UserVerificationToken>, IUserVerificationTokenRepository
    {
        public UserVerificationTokenRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Maybe<UserVerificationToken>> GetByTokenAsync(string token, CancellationToken cancellationToken = default) =>
            await DbContext.Set<UserVerificationToken>()
                .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);

        public async  Task<Maybe<UserVerificationToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
            await DbContext.Set<UserVerificationToken>()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}