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
        private readonly IBlobService _storageService;
        private readonly IProductRepository _productRepository;
        private readonly IProductImageFileRepository _productImageFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadProductImageFileCommandHandler(
            IBlobService blobService,
            IProductRepository productRepository,
            IProductImageFileRepository productImageFileRepository,
            IUnitOfWork unitOfWork)
        {
            _storageService = blobService;
            _productRepository = productRepository;
            _productImageFileRepository = productImageFileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UploadProductImageFileCommand request, CancellationToken cancellationToken)
        {
            // productın varlığını kontrol et
            Maybe<Product> maybeProduct =
                await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (maybeProduct.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Product product = maybeProduct.Value;

            // önceki resmi varsa sil

            if (product.ProductImageFileId is not null)
            {
                await _storageService.DeleteAsync(product.ProductImageFileId ?? Guid.Empty, StorageContainerNames.ProductImages, cancellationToken);
            }

            // yeni resmi yükle

            using Stream stream = request.ImageFile.OpenReadStream();
            string contentType = request.ImageFile.ContentType;

            Guid fileId = await _storageService.UploadAsync(stream, contentType, StorageContainerNames.ProductImages, cancellationToken);

            // product image file ekle

            ProductImageFile productImageFile = new()
            {
                Id = fileId,
                ProductId = product.Id,
                Name = request.ImageFile.FileName,
                ContainerName = StorageContainerNames.ProductImages,
                ContentType = request.ImageFile.ContentType,
                Size = request.ImageFile.Length
            };

            _productImageFileRepository.Insert(productImageFile);

            // product image file id güncelle

            product.ProductImageFileId = fileId;

            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
