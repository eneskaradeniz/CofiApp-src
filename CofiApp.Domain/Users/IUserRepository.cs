using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.Users
{
    public interface IUserRepository
    {
        Task<Maybe<User>> GetByIdAsync(Guid userId);

        Task<Maybe<User>> GetByEmailAsync(string email);

        Task<bool> IsEmailUniqueAsync(string email);

        void Insert(User user);

        void Update(User user);

        void Remove(User user);
    }
}
