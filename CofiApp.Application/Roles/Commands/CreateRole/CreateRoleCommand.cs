using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : ICommand<Result>
    {
        public CreateRoleCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
