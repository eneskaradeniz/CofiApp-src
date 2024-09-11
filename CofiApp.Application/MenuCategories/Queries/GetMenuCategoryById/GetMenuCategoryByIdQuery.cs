using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.MenuCategories.Queries.GetMenuCategoryById
{
    public class GetMenuCategoryByIdQuery : IQuery<Maybe<MenuCategoryResponse>>
    {
        public GetMenuCategoryByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
