using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Products.Queries.PublicGetProductById
{
    public class PublicGetProductByIdQuery : IQuery<Maybe<PublicProductWithOptionsResponse>>
    {
        public PublicGetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
