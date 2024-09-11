using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Products.Queries.GetProducts
{
    public class GetProductsQuery : IQuery<Maybe<PagedList<ProductResponse>>>
    {
        public GetProductsQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}
