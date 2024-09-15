using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Users;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Users.Queries.GetUsers
{
    public class GetUsersQuery : IQuery<Maybe<PagedList<UserResponse>>>
    {
        public GetUsersQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        // TODO: Page and PageSize should be validated (min max vs.) AND default values should be set.
        public int Page { get; }
        public int PageSize { get; }
    }
}
