using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.MenuCategories.Commands.CreateMenuCategory
{
    public class CreateMenuCategoryCommandValidator : AbstractValidator<CreateMenuCategoryCommand>
    {
        public CreateMenuCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithError(ValidationErrors.MenuCategories.NameIsRequired)
                .MaximumLength(100).WithError(ValidationErrors.MenuCategories.NameIsTooLong);
        }
    }
}
