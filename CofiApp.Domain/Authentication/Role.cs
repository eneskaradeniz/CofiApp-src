using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Users;

namespace CofiApp.Domain.Authentication
{
    public class Role : Entity, IAuditableEntity
    {
        public string Name { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; }

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
