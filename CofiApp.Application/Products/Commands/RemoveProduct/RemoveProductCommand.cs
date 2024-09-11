using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Products.Commands.RemoveProduct
{
    public class RemoveProductCommand : ICommand<Result>
    {
        public RemoveProductCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
