using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Users;

namespace CofiApp.Domain.UserVerificationTokens
{
    public class UserVerificationToken : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOnUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string TokenType { get; set; }
        public bool IsUsed { get; set; }

        public User User { get; set; }
    }
}
