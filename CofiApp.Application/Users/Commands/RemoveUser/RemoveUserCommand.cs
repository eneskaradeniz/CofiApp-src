using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Users.Commands.RemoveUser
{
    public class RemoveUserCommand : ICommand<Result>
    {
        public RemoveUserCommand(Guid userId) => UserId = userId;

        public Guid UserId { get; }
    }
}
