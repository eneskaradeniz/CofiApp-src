using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.ProductOptionGroups;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.ProductOptionGroups.Queries.GetProductOptionGroupsWithOptions
{
    public class GetProductOptionGroupsWithOptionsQuery : IQuery<Maybe<List<ProductOptionGroupsWithOptionsResponse>>>
    {
        public GetProductOptionGroupsWithOptionsQuery(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; set; }
    }
}
