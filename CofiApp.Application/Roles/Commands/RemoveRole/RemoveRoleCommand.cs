using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Roles.Commands.RemoveRole
{
    public class RemoveRoleCommand : ICommand<Result>
    {
        public RemoveRoleCommand(Guid roleId)
        {
            RoleId = roleId;
        }

        public Guid RoleId { get; }
    }
}
