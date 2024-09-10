using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Users;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, Maybe<PagedList<UserResponse>>>
    {
        private readonly IDbContext _dbContext;

        public GetUsersQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<PagedList<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<UserResponse> query = _dbContext.Set<User>()
                .AsNoTracking()
                .Select(x => new UserResponse()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    CreatedOnUtc = x.CreatedOnUtc,
                });

            var totalCount = await query.CountAsync(cancellationToken);

            UserResponse[] responseArray = await query
                .Skip((request.Page -1 ) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new PagedList<UserResponse>(responseArray, request.Page, request.PageSize, totalCount);
        }
    }
}
