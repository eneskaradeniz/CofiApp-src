using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.ProductOptionGroups.Commands.CreateProductOptionGroup
{
    public class CreateProductOptionGroupCommandValidator : AbstractValidator<CreateProductOptionGroupCommand>
    {
        public CreateProductOptionGroupCommandValidator()
        {
            RuleFor(x => x.ProductId)
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
