using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.UserVerificationTokens
{
    public interface IUserVerificationTokenRepository
    {
        Task<Maybe<UserVerificationToken>> GetByUserIdAsync(Guid userId);

        Task<Maybe<UserVerificationToken>> GetByTokenAsync(string token);

        void Insert(UserVerificationToken userVerificationToken);

        void Update(UserVerificationToken userVerificationToken);
    }
}
