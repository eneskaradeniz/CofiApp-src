using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Baskets.Commands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandHandler : ICommandHandler<UpdateBasketItemQuantityCommand, Result>
    {
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBasketItemQuantityCommandHandler(IBasketItemRepository basketItemRepository, IUnitOfWork unitOfWork)
        {
            _basketItemRepository = basketItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateBasketItemQuantityCommand request, CancellationToken cancellationToken)
        {
            Maybe<BasketItem> maybeBasketItem = await _basketItemRepository.GetByIdAsync(request.BasketItemId, cancellationToken);

            if (maybeBasketItem.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            BasketItem basketItem = maybeBasketItem.Value;

            basketItem.Quantity = request.IsIncrease ? basketItem.Quantity + 1 : basketItem.Quantity - 1;

            if (basketItem.Quantity <= 0)
            {
                _basketItemRepository.Remove(basketItem);
            }
            else
            {
                _basketItemRepository.Update(basketItem);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
