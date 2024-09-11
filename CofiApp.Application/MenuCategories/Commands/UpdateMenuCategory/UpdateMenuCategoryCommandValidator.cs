using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.MenuCategories.Commands.UpdateMenuCategory
{
    public class UpdateMenuCategoryCommandValidator : AbstractValidator<UpdateMenuCategoryCommand>
    {
        public UpdateMenuCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithError(ValidationErrors.MenuCategories.IdIsRequired);

            RuleFor(x => x.Name)
                .NotEmpty().WithError(ValidationErrors.MenuCategories.NameIsRequired)
                .MaximumLength(100).WithError(ValidationErrors.MenuCategories.NameIsTooLong);
        }
    }
}
