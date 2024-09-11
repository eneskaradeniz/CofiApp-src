using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.ProductOptionGroups.Commands.RemoveProductOptionGroup
{
    public class RemoveProductOptionGroupCommand : ICommand<Result>
    {
        public RemoveProductOptionGroupCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
