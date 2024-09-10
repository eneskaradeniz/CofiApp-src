using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Users;

namespace CofiApp.Domain.UserRefreshTokens
{
    public class UserRefreshToken : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOnUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public User User { get; set; }
    }
}
