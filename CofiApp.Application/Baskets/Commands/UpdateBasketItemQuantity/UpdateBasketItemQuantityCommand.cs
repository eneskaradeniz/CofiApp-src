using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Baskets.Commands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommand : ICommand<Result>
    {
        public UpdateBasketItemQuantityCommand(Guid basketItemId, bool isIncrease)
        {
            BasketItemId = basketItemId;
            IsIncrease = isIncrease;
        }
        public Guid BasketItemId { get; set; }
        public bool IsIncrease { get; }
    }
}
