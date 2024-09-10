using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Roles.Queries.GetRolesPermissions
{
    public class GetRolesPermissionsQuery : IQuery<Maybe<List<RoleResponse>>>
    {
    }
}
