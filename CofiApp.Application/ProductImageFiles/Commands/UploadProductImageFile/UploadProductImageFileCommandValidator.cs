using CofiApp.Application.Core.Errors;
using CofiApp.Application.Core.Extensions;
using FluentValidation;

namespace CofiApp.Application.ProductImageFiles.Commands.UploadProductImageFile
{
    public class UploadProductImageFileCommandValidator : AbstractValidator<UploadProductImageFileCommand>
    {
        public UploadProductImageFileCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithError(ValidationErrors.ProductImageFiles.ProductIdIsRequired);
            RuleFor(x => x.ImageFile)
                .NotNull().WithError(ValidationErrors.ProductImageFiles.ImageFileIsRequired);
        }
    }
}
