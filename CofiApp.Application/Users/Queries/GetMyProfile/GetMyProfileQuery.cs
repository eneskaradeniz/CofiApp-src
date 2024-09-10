using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Users;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Users.Queries.GetMyProfile
{
    public class GetMyProfileQuery : IQuery<Result<MyProfileResponse>>
    {
        
    }
}
