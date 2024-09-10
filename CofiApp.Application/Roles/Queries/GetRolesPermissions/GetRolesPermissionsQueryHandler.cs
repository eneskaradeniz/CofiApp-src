using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Roles.Queries.GetRolesPermissions
{
    public class GetRolesPermissionsQueryHandler : IQueryHandler<GetRolesPermissionsQuery, Maybe<List<RoleResponse>>>
    {
        private readonly IDbContext _dbContext;

        public GetRolesPermissionsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<List<RoleResponse>>> Handle(GetRolesPermissionsQuery request, CancellationToken cancellationToken)
        {
            List<RoleResponse> values = await _dbContext.Set<Role>()
                .Include(x => x.Permissions)
                .Select(x => new RoleResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOnUtc = x.CreatedOnUtc,
                    Permissions = x.Permissions.Select(p => new PermissionResponse()
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Maybe<List<RoleResponse>>.From(values);
        }
    }
}
