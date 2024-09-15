using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.BasketItemOptionGroups;
using CofiApp.Domain.BasketItemOptions;
using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Baskets;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.OrderItemOptionGroups;
using CofiApp.Domain.OrderItemOptions;
using CofiApp.Domain.OrderItems;
using CofiApp.Domain.Orders;

namespace CofiApp.Application.Orders.Commands.CreateCustomerOrder
{
    public class CreateCustomerOrderCommandHandler : ICommandHandler<CreateCustomerOrderCommand, Result>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderItemOptionGroupRepository _orderItemOptionGroupRepository;
        private readonly IOrderItemOptionRepository _orderItemOptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public CreateCustomerOrderCommandHandler(IBasketRepository basketRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IOrderItemOptionGroupRepository orderItemOptionGroupRepository, IOrderItemOptionRepository orderItemOptionRepository, IUnitOfWork unitOfWork, IUserIdentifierProvider userIdentifierProvider)
        {
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _orderItemOptionGroupRepository = orderItemOptionGroupRepository;
            _orderItemOptionRepository = orderItemOptionRepository;
            _unitOfWork = unitOfWork;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result> Handle(CreateCustomerOrderCommand command, CancellationToken cancellationToken)
        {
            // kullanıcının aktif basketi var mı kontrol et
            Maybe<Basket> maybeBasket = await _basketRepository.GetActiveBasketByUserIdAsync(_userIdentifierProvider.UserId, cancellationToken);

            if (maybeBasket.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Basket basket = maybeBasket.Value;

            // baskette ürün var mı kontrol et
            if (basket.BasketItems.Count == 0)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            // sipariş oluştur (Basket, BasketItem, BasketItemOptionGroup ve BasketItemOptionItem tablolarını Order, OrderItem, OrderItemOptionGroup ve OrderItemOptionItem tablolarına çevir)

            Order newOrder = new()
            {
                Id = Guid.NewGuid(),
                UserId = _userIdentifierProvider.UserId,
                BasketId = basket.Id,
                Status = OrderStatus.Pending,
                TotalPrice = basket.TotalPrice
            };

            // orderın içindeki order itemları oluştur

            foreach (BasketItem basketItem in basket.BasketItems)
            {
                OrderItem newOrderItem = new()
                {
                    Id = Guid.NewGuid(),
                    OrderId = newOrder.Id,
                    ProductId = basketItem.ProductId,
                    ProductName = basketItem.Product.Name,
                    ProductPrice = basketItem.Product.Price,
                    Quantity = basketItem.Quantity,
                    TotalPrice = basketItem.TotalPrice
                };

                // order itemın içindeki order item option grupları oluştur

                foreach (BasketItemOptionGroup basketItemOptionGroup in basketItem.BasketItemOptionGroups)
                {
                    OrderItemOptionGroup newOrderItemOptionGroup = new()
                    {
                        Id = Guid.NewGuid(),
                        OrderItemId = newOrderItem.Id,
                        ProductOptionGroupId = basketItemOptionGroup.ProductOptionGroupId,
                        ProductOptionGroupName = basketItemOptionGroup.ProductOptionGroup.Name,
                        ProductOptionGroupIsRequired = basketItemOptionGroup.ProductOptionGroup.IsRequired,
                        ProductOptionGroupAllowMultiple = basketItemOptionGroup.ProductOptionGroup.AllowMultiple
                    };

                    // order item option grubunun içindeki order item optionları oluştur

                    foreach (BasketItemOption basketItemOptionItem in basketItemOptionGroup.BasketItemOptions)
                    {
                        OrderItemOption newOrderItemOption = new()
                        {
                            Id = Guid.NewGuid(),
                            OrderItemOptionGroupId = newOrderItemOptionGroup.Id,
                            ProductOptionId = basketItemOptionItem.ProductOptionId,
                            ProductOptionName = basketItemOptionItem.ProductOption.Name,
                            ProductOptionPrice = basketItemOptionItem.ProductOption.Price
                        };

                        _orderItemOptionRepository.Insert(newOrderItemOption);
                    }

                    _orderItemOptionGroupRepository.Insert(newOrderItemOptionGroup);
                }

                _orderItemRepository.Insert(newOrderItem);
            }

            _orderRepository.Insert(newOrder);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
