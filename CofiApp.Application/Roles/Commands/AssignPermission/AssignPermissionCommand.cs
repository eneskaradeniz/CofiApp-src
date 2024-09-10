using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Roles.Commands.AssignPermission
{
    public class AssignPermissionCommand : ICommand<Result>
    {
        public AssignPermissionCommand(Guid roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }

        public Guid RoleId { get; }
        public int PermissionId { get; }
    }
}
