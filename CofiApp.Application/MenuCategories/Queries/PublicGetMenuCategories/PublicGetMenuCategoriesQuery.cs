using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.MenuCategories.Queries.PublicGetMenuCategories
{
    public class PublicGetMenuCategoriesQuery : IQuery<Maybe<List<PublicMenuCategoryResponse>>>
    {
    }
}
