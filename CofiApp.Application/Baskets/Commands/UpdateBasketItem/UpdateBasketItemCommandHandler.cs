using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.BasketItemOptionGroups;
using CofiApp.Domain.BasketItemOptions;
using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Baskets.Commands.UpdateBasketItem
{
    public class UpdateBasketItemCommandHandler : ICommandHandler<UpdateBasketItemCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IBasketItemOptionGroupRepository _basketItemOptionGroupRepository;
        private readonly IBasketItemOptionRepository _basketItemOptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBasketItemCommandHandler(IDbContext dbContext, IBasketItemRepository basketItemRepository, IBasketItemOptionGroupRepository basketItemOptionGroupRepository, IBasketItemOptionRepository basketItemOptionRepository, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _basketItemRepository = basketItemRepository;
            _basketItemOptionGroupRepository = basketItemOptionGroupRepository;
            _basketItemOptionRepository = basketItemOptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            // basketitemin varlığını kontrol et
            Maybe<BasketItem> maybeBasketItem = await _dbContext.Set<BasketItem>()
                .Include(bi => bi.BasketItemOptionGroups)
                    .ThenInclude(biog => biog.BasketItemOptions)
                .Where(bi => bi.Id == request.BasketItemId)
                .FirstOrDefaultAsync(cancellationToken);               

            if (maybeBasketItem.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            BasketItem basketItem = maybeBasketItem.Value;

            // basketitemin varolan opsiyonlarını gruplarını sil

            _basketItemOptionGroupRepository.RemoveRange(basketItem.BasketItemOptionGroups.ToList());
            _basketItemOptionRepository.RemoveRange(basketItem.BasketItemOptionGroups.SelectMany(biog => biog.BasketItemOptions).ToList());

            // basketitemin yeni opsiyonlarını gruplarını ekle

            foreach (var option in request.ProductOptions)
            {
                BasketItemOptionGroup newBasketItemOptionGroup = new()
                {
                    Id = Guid.NewGuid(),
                    BasketItemId = basketItem.Id,
                    ProductOptionGroupId = option.ProductOptionGroupId
                };

                _basketItemOptionGroupRepository.Insert(newBasketItemOptionGroup);

                BasketItemOption newBasketItemOption = new()
                {
                    Id = Guid.NewGuid(),
                    BasketItemOptionGroupId = newBasketItemOptionGroup.Id,
                    ProductOptionId = option.ProductOptionId
                };

                _basketItemOptionRepository.Insert(newBasketItemOption);
            }

            // basketitemin miktarını güncelle

            basketItem.Quantity = request.Quantity;

            _basketItemRepository.Update(basketItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
