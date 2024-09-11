using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.ProductOptionGroups;
using CofiApp.Domain.Products;

namespace CofiApp.Application.ProductOptionGroups.Commands.CreateProductOptionGroup
{
    public class CreateProductOptionGroupsCommandHandler :
        ICommandHandler<CreateProductOptionGroupCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionGroupRepository _productOptionGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductOptionGroupsCommandHandler(IProductOptionGroupRepository productOptionGroupRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _productOptionGroupRepository = productOptionGroupRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(CreateProductOptionGroupCommand request, CancellationToken cancellationToken)
        {
            if (!await _productRepository.ProductExistsAsync(request.ProductId))
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            ProductOptionGroup productOptionGroup = new()
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Name = request.Name,
                IsRequired = request.IsRequired,
                AllowMultiple = request.AllowMultiple
            };

            _productOptionGroupRepository.Insert(productOptionGroup);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
