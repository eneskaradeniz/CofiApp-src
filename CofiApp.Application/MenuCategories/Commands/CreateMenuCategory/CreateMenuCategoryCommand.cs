using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.MenuCategories.Commands.CreateMenuCategory
{
    public class CreateMenuCategoryCommand : ICommand<Result>
    {
        public CreateMenuCategoryCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
