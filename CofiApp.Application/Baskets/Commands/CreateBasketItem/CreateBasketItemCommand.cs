using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Baskets;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Baskets.Commands.CreateBasketItem
{
    public class CreateBasketItemCommand : ICommand<Result>
    {
        public CreateBasketItemCommand(Guid productId, List<ProductOptionDto> productOptions, int quantity)
        {
            ProductId = productId;
            ProductOptions = productOptions;
            Quantity = quantity;
        }

        public Guid ProductId { get; }
        public List<ProductOptionDto> ProductOptions { get; }
        public int Quantity { get; }
    }
}
