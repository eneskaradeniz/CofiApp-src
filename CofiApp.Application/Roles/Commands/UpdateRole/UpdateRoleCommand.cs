using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : ICommand<Result>
    {
        public UpdateRoleCommand(Guid roleId, string name)
        {
            RoleId = roleId;
            Name = name;
        }

        public Guid RoleId { get; }
        public string Name { get; }
    }
}
