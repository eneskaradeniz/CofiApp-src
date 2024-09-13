using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Baskets;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Baskets.Queries.GetActiveBasketByUser
{
    public class GetActiveBasketByUserQuery : IQuery<Maybe<BasketResponse>>
    {
        public GetActiveBasketByUserQuery()
        {
            
        }
    }
}
