using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Http;

namespace CofiApp.Application.ProductImageFiles.Commands.UploadProductImageFile
{
    public class UploadProductImageFileCommand : ICommand<Result>
    {
        public UploadProductImageFileCommand(Guid productId, IFormFile imageFile)
        {
            ProductId = productId;
            ImageFile = imageFile;
        }
        public Guid ProductId { get; set; }
        public IFormFile ImageFile { get; }
    }
}
