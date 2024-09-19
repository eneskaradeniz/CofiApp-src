using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.ProductImageFiles.Commands.RemoveProductImageFile
{
    public class RemoveProductImageFileCommandValidator : AbstractValidator<RemoveProductImageFileCommand>
    {
        public RemoveProductImageFileCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithError(ValidationErrors.ProductImageFiles.ProductIdIsRequired);
        }
    }
}
