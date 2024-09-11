using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.ProductOptionGroups.Commands.CreateProductOptionGroup
{
    public class CreateProductOptionGroupCommand : ICommand<Result>
    {
        public CreateProductOptionGroupCommand(Guid productId, string name, bool ısRequired, bool allowMultiple)
        {
            ProductId = productId;
            Name = name;
            IsRequired = ısRequired;
            AllowMultiple = allowMultiple;
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }
    }
}
