using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Orders;

namespace CofiApp.Application.Orders.Commands.CompleteShopOrder
{
    public class CompleteShopOrderCommandHandler : ICommandHandler<CompleteShopOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteShopOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CompleteShopOrderCommand request, CancellationToken cancellationToken)
        {
            Maybe<Order> maybeOrder = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

            if (maybeOrder.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Order order = maybeOrder.Value;

            if (order.Status != OrderStatus.Processing)
            {
                return Result.Failure(DomainErrors.Order.OrderCannotBeCompleted);
            }

            order.UpdateStatus(OrderStatus.Completed);

            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
