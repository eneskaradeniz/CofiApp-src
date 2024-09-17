using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Orders;

namespace CofiApp.Application.Orders.Commands.CancelShopOrder
{
    public class CancelShopOrderCommandHandler : ICommandHandler<CancelShopOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelShopOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CancelShopOrderCommand request, CancellationToken cancellationToken)
        {
            Maybe<Order> maybeOrder = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

            if (maybeOrder.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Order order = maybeOrder.Value;

            if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
            {
                return Result.Failure(DomainErrors.Order.OrderCannotBeCancelled);
            }

            order.UpdateStatus(OrderStatus.Cancelled);

            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
