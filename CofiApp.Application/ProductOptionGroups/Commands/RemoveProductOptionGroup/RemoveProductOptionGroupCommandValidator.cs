using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.ProductOptionGroups.Commands.RemoveProductOptionGroup
{
    public class RemoveProductOptionGroupCommandValidator : AbstractValidator<RemoveProductOptionGroupCommand>
    {
        public RemoveProductOptionGroupCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithError(ValidationErrors.ProductOptionGroups.IdIsRequired);
        }
    }
}
