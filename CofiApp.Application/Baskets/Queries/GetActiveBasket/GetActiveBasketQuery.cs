using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Baskets;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Baskets.Queries.GetActiveBasketByUser
{
    public class GetActiveBasketQuery : IQuery<Maybe<BasketResponse>>
    {
        public GetActiveBasketQuery()
        {
            
        }
    }
}
