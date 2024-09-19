using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.ProductImageFiles.Commands.RemoveProductImageFile
{
    public class RemoveProductImageFileCommand : ICommand<Result>
    {
        public RemoveProductImageFileCommand(Guid productId)
        {
            ProductId = productId;
        }
        public Guid ProductId { get; set; }
    }
}
