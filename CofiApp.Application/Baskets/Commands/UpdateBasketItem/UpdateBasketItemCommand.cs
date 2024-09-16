using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.ProductOptions;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Baskets.Commands.UpdateBasketItem
{
    public class UpdateBasketItemCommand : ICommand<Result>
    {
        public UpdateBasketItemCommand(Guid basketItemId, List<ProductOptionDto> productOptions, int quantity)
        {
            BasketItemId = basketItemId;
            ProductOptions = productOptions;
            Quantity = quantity;
        }

        public Guid BasketItemId { get; }
        public List<ProductOptionDto> ProductOptions { get; }
        public int Quantity { get; }
    }
}
