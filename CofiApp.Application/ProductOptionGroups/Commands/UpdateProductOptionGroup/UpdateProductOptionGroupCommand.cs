using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.ProductOptionGroups.Commands.UpdateProductOptionGroup
{
    public class UpdateProductOptionGroupCommand : ICommand<Result>
    {
        public UpdateProductOptionGroupCommand(Guid id, string name, bool isRequired, bool allowMultiple)
        {
            Id = id;
            Name = name;
            IsRequired = isRequired;
            AllowMultiple = allowMultiple;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }
    }
}
