using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Application.Abstractions.Storage;
using CofiApp.Application.Constants;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.ProductImageFiles;
using CofiApp.Domain.Products;

namespace CofiApp.Application.ProductImageFiles.Commands.RemoveProductImageFile
{
    public class RemoveProductImageFileCommandHandler : ICommandHandler<RemoveProductImageFileCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageFileRepository _productImageFileRepository;
        private readonly IBlobService _blobService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductImageFileCommandHandler(IProductRepository productRepository, IProductImageFileRepository productImageFileRepository, IBlobService blobService, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productImageFileRepository = productImageFileRepository;
            _blobService = blobService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveProductImageFileCommand request, CancellationToken cancellationToken)
        {
            Maybe<Product> maybeProduct = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (maybeProduct.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Product product = maybeProduct.Value;

            if (product.ProductImageFileId is null)
            {
                return Result.Failure(DomainErrors.ProductImageFile.AlreadyRemoved);
            }

            Maybe<ProductImageFile> maybeProductImageFile = await _productImageFileRepository.GetByIdAsync(product.ProductImageFileId ?? Guid.Empty, cancellationToken);

            if (maybeProductImageFile.HasNoValue)
            {
                return Result.Failure(DomainErrors.ProductImageFile.AlreadyRemoved);
            }

            ProductImageFile productImageFile = maybeProductImageFile.Value;

            await _blobService.DeleteAsync(product.ProductImageFileId ?? Guid.Empty, StorageContainerNames.ProductImages, cancellationToken);

            _productImageFileRepository.Remove(productImageFile);

            product.ProductImageFileId = Guid.Empty;

            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
