using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.ProductOptions.Commands.RemoveProductOption
{
    public class RemoveProductOptionCommand : ICommand<Result>
    {
        public RemoveProductOptionCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
