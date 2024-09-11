using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IQuery<Maybe<ProductResponse>>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
