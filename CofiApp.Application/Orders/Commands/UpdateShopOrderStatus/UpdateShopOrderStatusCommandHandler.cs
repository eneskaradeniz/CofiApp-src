using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Orders;

namespace CofiApp.Application.Orders.Commands.UpdateShopOrderStatus
{
    public class UpdateShopOrderStatusCommandHandler : ICommandHandler<UpdateShopOrderStatusCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateShopOrderStatusCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateShopOrderStatusCommand command, CancellationToken cancellationToken)
        {
            Maybe<Order> maybeOrder = await _orderRepository.GetByIdAsync(command.OrderId);
            
            if (maybeOrder.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Order order = maybeOrder.Value;

            order.UpdateStatus(command.Status);

            _orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
