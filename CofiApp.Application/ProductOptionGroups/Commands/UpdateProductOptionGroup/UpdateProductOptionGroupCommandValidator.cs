using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.ProductOptionGroups.Commands.UpdateProductOptionGroup
{
    public class UpdateProductOptionGroupCommandValidator : AbstractValidator<UpdateProductOptionGroupCommand>
    {
        public UpdateProductOptionGroupCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithError(ValidationErrors.ProductOptionGroups.IdIsRequired);

            RuleFor(x => x.Name)
                .NotEmpty().WithError(ValidationErrors.ProductOptionGroups.NameIsRequired)
                .MaximumLength(100).WithError(ValidationErrors.ProductOptionGroups.NameIsTooLong);

            RuleFor(x => x.IsRequired)
                .NotNull().WithError(ValidationErrors.ProductOptionGroups.IsRequiredIsRequired);

            RuleFor(x => x.AllowMultiple)
                .NotNull().WithError(ValidationErrors.ProductOptionGroups.AllowMultipleIsRequired);
        }
    }
}
