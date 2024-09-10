using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.UserRefreshTokens
{
    public interface IUserRefreshTokenRepository
    {
        void Insert(UserRefreshToken userRefreshToken);
        void Update(UserRefreshToken userRefreshToken);

        Task<Maybe<UserRefreshToken>> GetByUserIdAsync(Guid userId);
        Task<Maybe<UserRefreshToken>> GetByTokenAsync(string token);
    }
}
