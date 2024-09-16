using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using CofiApp.Contracts.MenuCategories;
using FluentValidation;

namespace CofiApp.Application.MenuCategories.Commands.UpdateMenuCategoriesDisplayOrder
{
    public class UpdateMenuCategoriesDisplayOrderCommandValidator : AbstractValidator<UpdateMenuCategoriesDisplayOrderCommand>
    {
        public UpdateMenuCategoriesDisplayOrderCommandValidator()
        {
            RuleFor(x => x.UpdateMenuCategoriesDisplayOrders)
                .NotNull()
                    .WithError(ValidationErrors.MenuCategories.UpdateMenuCategoriesDisplayOrdersIsRequired)
                .NotEmpty()
                    .WithError(ValidationErrors.MenuCategories.UpdateMenuCategoriesDisplayOrdersIsRequired)
                .Must(BeUniqueIdsAndDisplayOrders)
                    .WithError(ValidationErrors.MenuCategories.UpdateMenuCategoriesDisplayOrdersMustBeUnique);
        }

        private bool BeUniqueIdsAndDisplayOrders(List<UpdateMenuCategoriesDisplayOrderDto> menuCategories)
        {
            var displayOrders = menuCategories.Select(o => o.DisplayOrder);
            var ids = menuCategories.Select(o => o.Id);

            var distinctDisplayOrdersCount = displayOrders.Distinct().Count();
            var distinctIdsCount = ids.Distinct().Count();

            return distinctDisplayOrdersCount == menuCategories.Count && distinctIdsCount == menuCategories.Count;
        }
    }
}
