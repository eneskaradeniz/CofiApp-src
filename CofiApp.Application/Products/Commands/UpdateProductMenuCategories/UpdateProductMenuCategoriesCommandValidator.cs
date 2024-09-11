using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.Products.Commands.UpdateProductMenuCategories
{
    public class UpdateProductMenuCategoriesCommandValidator : 
        AbstractValidator<UpdateProductMenuCategoriesCommand>
    {
        public UpdateProductMenuCategoriesCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithError(ValidationErrors.ProductMenuCategories.ProductIdIsRequired);

            RuleFor(x => x.MenuCategoryIds)
                .NotEmpty().WithError(ValidationErrors.ProductMenuCategories.MenuCategoryIdsIsRequired);
        }
    }
}
