using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.MenuCategories.Queries.PublicGetMenuCategoryByIdWithProducts
{
    public class PublicGetMenuCategoryByIdWithProductsQuery : IQuery<Maybe<PublicMenuCategoryWithProductsResponse>>
    {
        public PublicGetMenuCategoryByIdWithProductsQuery(Guid id, int page, int pageSize)
        {
            Id = id;
            Page = page;
            PageSize = pageSize;
        }

        public Guid Id { get; }
        public int Page { get; }
        public int PageSize { get; }
    }
}
