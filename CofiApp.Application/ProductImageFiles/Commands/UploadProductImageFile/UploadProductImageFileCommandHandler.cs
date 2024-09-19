using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Application.Abstractions.Storage;
using CofiApp.Application.Constants;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.ProductImageFiles;
using CofiApp.Domain.Products;

namespace CofiApp.Application.ProductImageFiles.Commands.UploadProductImageFile
{
    public class UploadProductImageFileCommandHandler : ICommandHandler<UploadProductImageFileCommand, Result>
    {
        private readonly IStorageService _storageService;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageFileRepository _productImageFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadProductImageFileCommandHandler(IStorageService storageService, IProductRepository productRepository, IProductImageFileRepository productImageFileRepository, IUnitOfWork unitOfWork)
        {
            _storageService = storageService;
            _productRepository = productRepository;
            _productImageFileRepository = productImageFileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UploadProductImageFileCommand request, CancellationToken cancellationToken)
        {
            Maybe<Product> maybeProduct =
                await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (maybeProduct.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Product product = maybeProduct.Value;

            if (product.ProductImageFileId is not null)
            {
                Maybe<ProductImageFile> maybeProductImageFile = await _productImageFileRepository.GetByIdAsync(product.ProductImageFileId ?? Guid.Empty, cancellationToken);

                if (maybeProductImageFile.HasNoValue)
                {
                    return Result.Failure(DomainErrors.General.NotFound);
                }

                ProductImageFile productImageFile = maybeProductImageFile.Value;

                await _storageService.DeleteAsync(productImageFile.Id, StorageContainerNames.ProductImages, cancellationToken);

                _productImageFileRepository.Remove(productImageFile);

                product.ProductImageFileId = null;
            }

            using Stream stream = request.ImageFile.OpenReadStream();
            string contentType = request.ImageFile.ContentType;

            Guid fileId = await _storageService.UploadAsync(stream, contentType, StorageContainerNames.ProductImages, cancellationToken);

            ProductImageFile newProductImageFile = new()
            {
                Id = fileId,
                ProductId = product.Id,
                Name = request.ImageFile.FileName,
                Path = StorageContainerNames.ProductImages,
                ContentType = request.ImageFile.ContentType,
                Size = request.ImageFile.Length,
                StorageType = _storageService.StorageType
            };

            _productImageFileRepository.Insert(newProductImageFile);

            product.ProductImageFileId = fileId;

            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
