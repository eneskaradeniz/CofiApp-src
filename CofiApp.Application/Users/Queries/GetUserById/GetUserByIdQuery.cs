using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Users;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IQuery<Maybe<UserResponse>>
    {
        public GetUserByIdQuery(Guid userId) => UserId = userId;
        public Guid UserId { get; }
    }
}
