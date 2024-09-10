using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IQuery<Maybe<List<RoleResponse>>>
    {
    }
}
