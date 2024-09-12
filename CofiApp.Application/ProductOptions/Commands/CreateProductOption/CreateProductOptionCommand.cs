using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.ProductOptions.Commands.CreateProductOption
{
    public class CreateProductOptionCommand : ICommand<Result>
    {
        public CreateProductOptionCommand(Guid productOptionGroupId, string name, decimal price)
        {
            ProductOptionGroupId = productOptionGroupId;
            Name = name;
            Price = price;
        }

        public Guid ProductOptionGroupId { get;}
        public string Name { get; }
        public decimal Price { get; }
    }
}
