using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.MenuCategories.Commands.UpdateMenuCategoriesDisplayOrder
{
    public class UpdateMenuCategoriesDisplayOrderCommand : ICommand<Result>
    {
        public UpdateMenuCategoriesDisplayOrderCommand(List<UpdateMenuCategoriesDisplayOrderDto> updateMenuCategoriesDisplayOrders)
        {
            UpdateMenuCategoriesDisplayOrders = updateMenuCategoriesDisplayOrders;
        }

        public List<UpdateMenuCategoriesDisplayOrderDto> UpdateMenuCategoriesDisplayOrders { get; }
    }
}
