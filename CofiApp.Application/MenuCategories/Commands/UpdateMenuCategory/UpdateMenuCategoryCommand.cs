using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.MenuCategories.Commands.UpdateMenuCategory
{
    public class UpdateMenuCategoryCommand : ICommand<Result>
    {
        public UpdateMenuCategoryCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}
