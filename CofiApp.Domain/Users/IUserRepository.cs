using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.Users
{
    public interface IUserRepository
    {
        Task<Maybe<User>> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Maybe<User>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

        void Insert(User user);

        void Update(User user);

        void Remove(User user);
    }
}
