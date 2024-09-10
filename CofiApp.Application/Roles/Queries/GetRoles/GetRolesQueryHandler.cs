using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Authentication;
using CofiApp.Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Roles.Queries.GetRoles
{
    public class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, Maybe<List<RoleResponse>>>
    {
        private readonly IDbContext _dbContext;

        public GetRolesQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<List<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var values = await _dbContext.Set<Role>()
                .Select(x => new RoleResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOnUtc = x.CreatedOnUtc
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Maybe<List<RoleResponse>>.From(values);
        }
    }
}
