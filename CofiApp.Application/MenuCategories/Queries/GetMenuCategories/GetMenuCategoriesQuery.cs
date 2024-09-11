using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.MenuCategories.Queries.GetMenuCategories
{
    public class GetMenuCategoriesQuery : IQuery<Maybe<PagedList<MenuCategoryResponse>>>
    {
        public GetMenuCategoriesQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        public int Page { get; }
        public int PageSize { get; }
    }
}
