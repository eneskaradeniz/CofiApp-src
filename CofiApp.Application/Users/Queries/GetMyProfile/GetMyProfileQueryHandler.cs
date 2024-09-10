using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Users;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Users.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler : IQueryHandler<GetMyProfileQuery, Result<MyProfileResponse>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        public GetMyProfileQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        public async Task<Result<MyProfileResponse>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            if (_userIdentifierProvider.UserId == Guid.Empty)
            {
                return Result.Failure<MyProfileResponse>(DomainErrors.General.NotFound);
            }

            MyProfileResponse? user = await _dbContext.Set<User>()
                .Where(u => u.Id == _userIdentifierProvider.UserId)
                .Select(x => new MyProfileResponse()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return Result.Failure<MyProfileResponse>(DomainErrors.General.NotFound);
            }

            return Result.Success(user);
        }
    }
}
