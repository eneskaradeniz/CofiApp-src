using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Roles.Commands.AssignUser
{
    public class AssignUserCommand : ICommand<Result>
    {
        public AssignUserCommand(Guid roleId, Guid userId)
        {
            RoleId = roleId;
            UserId = userId;
        }

        public Guid RoleId { get; }

        public Guid UserId { get; }
    }
}
