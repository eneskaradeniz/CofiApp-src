using CofiApp.Domain.Authentication;
using CofiApp.Domain.Baskets;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Orders;
using CofiApp.Domain.UserRefreshTokens;
using CofiApp.Domain.UserVerificationTokens;

namespace CofiApp.Domain.Users
{
    public class User : Entity, ISoftDeletableEntity, IAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public DateTime? DeletedOnUtc { get; set; }
        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public ICollection<Basket> Baskets { get; set; }
        public ICollection<Order> Orders { get; set; }

        public UserRefreshToken UserRefreshToken { get; set; }
        public UserVerificationToken UserVerificationToken { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
