using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.ProductOptionGroups;
using CofiApp.Domain.ProductOptions;

namespace CofiApp.Application.ProductOptions.Commands.CreateProductOption
{
    public class CreateProductOptionCommandHandler : ICommandHandler<CreateProductOptionCommand, Result>
    {
        private readonly IProductOptionGroupRepository _productOptionGroupRepository;
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductOptionCommandHandler(IProductOptionGroupRepository productOptionGroupRepository, IProductOptionRepository productOptionRepository, IUnitOfWork unitOfWork)
        {
            _productOptionGroupRepository = productOptionGroupRepository;
            _productOptionRepository = productOptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateProductOptionCommand request, CancellationToken cancellationToken)
        {
            if(!await _productOptionGroupRepository.AnyAsync(request.ProductOptionGroupId, cancellationToken))
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            ProductOption productOption = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                ProductOptionGroupId = request.ProductOptionGroupId
            };

            _productOptionRepository.Insert(productOption);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
