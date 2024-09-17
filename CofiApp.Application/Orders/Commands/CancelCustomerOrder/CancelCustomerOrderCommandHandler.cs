using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Orders;

namespace CofiApp.Application.Orders.Commands.CancelCustomerOrder
{
    public class CancelCustomerOrderCommandHandler : ICommandHandler<CancelCustomerOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public CancelCustomerOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IUserIdentifierProvider userIdentifierProvider)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result> Handle(CancelCustomerOrderCommand command, CancellationToken cancellationToken)
        {
            Maybe<Order> maybeOrder = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);

            if (maybeOrder.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Order order = maybeOrder.Value;

            if (order.UserId != _userIdentifierProvider.UserId)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            if (order.Status != OrderStatus.Pending || order.Status == OrderStatus.Cancelled)
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
