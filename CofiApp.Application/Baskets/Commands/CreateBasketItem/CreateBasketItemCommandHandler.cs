using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.ProductOptions;
using CofiApp.Domain.BasketItemOptionGroups;
using CofiApp.Domain.BasketItemOptions;
using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Baskets;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Products;

namespace CofiApp.Application.Baskets.Commands.CreateBasketItem
{
    public class CreateBasketItemCommandHandler : ICommandHandler<CreateBasketItemCommand, Result>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IBasketItemOptionGroupRepository _basketItemOptionGroupRepository;
        private readonly IBasketItemOptionRepository _basketItemOptionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public CreateBasketItemCommandHandler(IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, IBasketItemOptionGroupRepository basketItemOptionGroupRepository, IBasketItemOptionRepository basketItemOptionRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IUserIdentifierProvider userIdentifierProvider)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _basketItemOptionGroupRepository = basketItemOptionGroupRepository;
            _basketItemOptionRepository = basketItemOptionRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            // Sepet kontrolü: Kullanıcının aktif sepeti var mı?
            Maybe<Basket> maybeBasket =
                await _basketRepository.GetActiveBasketByUserIdAsync(_userIdentifierProvider.UserId, cancellationToken);

            Basket? basket = maybeBasket.HasValue ? maybeBasket.Value : null;

            if (basket is null)
            {
                basket = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = _userIdentifierProvider.UserId,
                    Status = BasketStatus.Active
                };

                _basketRepository.Insert(basket);
            }

            // Ürün kontrolü: Eklemek istediği ürün (opsiyon grubu ve opsiyonlarıyla birlikte) basketta var mı?
            BasketItem? existingBasketItem = basket.BasketItems
                .FirstOrDefault(bi => bi.ProductId == request.ProductId &&
                                  AreOptionsSame(bi.BasketItemOptionGroups, request.ProductOptions));

            if (existingBasketItem is null)
            {
                // Ürün sepette yoksa ya da opsiyon grupları ve opsiyonlar farklıysa, yeni bir BasketItem eklenir
                Maybe<Product> maybeProduct = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

                if (maybeProduct.HasNoValue)
                {
                    return Result.Failure(DomainErrors.General.NotFound);
                }

                BasketItem newBasketItem = new()
                {
                    Id = Guid.NewGuid(),
                    BasketId = basket.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                foreach (var option in request.ProductOptions)
                {
                    BasketItemOptionGroup newBasketItemOptionGroup = new()
                    {
                        Id = Guid.NewGuid(),
                        BasketItemId = newBasketItem.Id,
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

                _basketItemRepository.Insert(newBasketItem);
            }
            else
            {
                // Ürün ve opsiyonlar sepette var, miktarı artır
                existingBasketItem.Quantity += request.Quantity;
                _basketItemRepository.Update(existingBasketItem);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        private bool AreOptionsSame(ICollection<BasketItemOptionGroup> existingOptionGroups, List<ProductOptionDto> newOptions)
        {
            var existingOptionIds = existingOptionGroups.SelectMany(g => g.BasketItemOptions)
                                                        .Select(o => new { o.ProductOptionId, o.BasketItemOptionGroup.ProductOptionGroupId })
                                                        .ToList();

            var newOptionIds = newOptions.Select(o => new { o.ProductOptionId, o.ProductOptionGroupId }).ToList();

            return existingOptionIds.Count == newOptionIds.Count && !existingOptionIds.Except(newOptionIds).Any();
        }
    }
}
