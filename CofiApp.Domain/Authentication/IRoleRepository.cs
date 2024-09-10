using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.Authentication
{
    public interface IRoleRepository
    {
        Task<bool> IsNameUniqueAsync(string name);
        Task<Maybe<Role>> GetByIdAsync(Guid id);
        void Insert(Role role);
        void Update(Role role);
        void Remove(Role role);
    }
}
