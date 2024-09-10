using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Users;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Maybe<UserResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetUserByIdQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                return Maybe<UserResponse>.None;
            }

            var response = await _dbContext.Set<User>()
                .Where(x => x.Id == request.UserId)
                .Select(x => new UserResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    CreatedOnUtc = x.CreatedOnUtc
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (response is null)
            {
                return Maybe<UserResponse>.None;
            }

            return response;
        }
    }
}
