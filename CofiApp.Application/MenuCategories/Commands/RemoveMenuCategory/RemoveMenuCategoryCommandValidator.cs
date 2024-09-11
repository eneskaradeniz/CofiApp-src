using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.MenuCategories.Commands.RemoveMenuCategory
{
    public class RemoveMenuCategoryCommandValidator : AbstractValidator<RemoveMenuCategoryCommand>
    {
        public RemoveMenuCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithError(ValidationErrors.MenuCategories.IdIsRequired);
        }
    }
}
