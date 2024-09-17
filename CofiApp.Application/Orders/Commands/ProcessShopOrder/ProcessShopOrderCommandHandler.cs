using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.EventBus;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Orders;

namespace CofiApp.Application.Orders.Commands.ProcessShopOrder
{
    public class ProcessShopOrderCommandHandler : ICommandHandler<ProcessShopOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;

        public ProcessShopOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(ProcessShopOrderCommand request, CancellationToken cancellationToken)
        {
            Maybe<Order> maybeOrder = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

            if (maybeOrder.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }
            Order order = maybeOrder.Value;

            if (order.Status != OrderStatus.Pending)
            {
                return Result.Failure(DomainErrors.Order.OrderCannotBeProcessed);
            }

            order.UpdateStatus(OrderStatus.Processing);

            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _eventBus.PublishAsync(new OrderProcessingEvent
            {
                Id = request.OrderId,
            }, cancellationToken);

            return Result.Success();
        }
    }
}
