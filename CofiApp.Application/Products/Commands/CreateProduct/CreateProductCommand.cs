using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : ICommand<Result>
    {
        public CreateProductCommand(string name, string? description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
        
        public string Name { get; }
        public string? Description { get; }
        public decimal Price { get; }
    }
}
