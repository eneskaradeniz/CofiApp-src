using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : ICommand<Result>
    {
        public UpdateProductCommand(Guid id, string name, string? description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string? Description { get; }
        public decimal Price { get; }
    }
}
