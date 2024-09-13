using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.UserVerificationTokens
{
    public interface IUserVerificationTokenRepository
    {
        Task<Maybe<UserVerificationToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Maybe<UserVerificationToken>> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

        void Insert(UserVerificationToken userVerificationToken);

        void Update(UserVerificationToken userVerificationToken);
    }
}
