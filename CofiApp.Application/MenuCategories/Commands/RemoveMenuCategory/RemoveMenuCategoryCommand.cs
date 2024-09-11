using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.MenuCategories.Commands.RemoveMenuCategory
{
    public class RemoveMenuCategoryCommand : ICommand<Result>
    {
        public RemoveMenuCategoryCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
