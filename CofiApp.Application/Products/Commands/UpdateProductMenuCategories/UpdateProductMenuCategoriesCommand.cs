using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Products.Commands.UpdateProductMenuCategories
{
    public class UpdateProductMenuCategoriesCommand : ICommand<Result>
    {
        public UpdateProductMenuCategoriesCommand(Guid productId, List<Guid> menuCategoryIds)
        {
            ProductId = productId;
            MenuCategoryIds = menuCategoryIds;
        }

        public Guid ProductId { get; }
        public List<Guid> MenuCategoryIds { get; }
    }
}
