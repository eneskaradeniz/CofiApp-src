using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Orders.Commands.CreateCustomerOrder
{
    public class CreateCustomerOrderCommand : ICommand<Result>
    {
        public CreateCustomerOrderCommand()
        {
        }
    }
}
