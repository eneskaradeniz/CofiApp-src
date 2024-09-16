using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.ProductOptionGroups;

namespace CofiApp.Application.ProductOptionGroups.Commands.RemoveProductOptionGroup
{
    public class RemoveProductOptionGroupCommandHandler : ICommandHandler<RemoveProductOptionGroupCommand, Result>
    {
        private readonly IProductOptionGroupRepository _productOptionGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductOptionGroupCommandHandler(IProductOptionGroupRepository productOptionGroupRepository, IUnitOfWork unitOfWork)
        {
            _productOptionGroupRepository = productOptionGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveProductOptionGroupCommand request, CancellationToken cancellationToken)
        {
            Maybe<ProductOptionGroup> maybeProductOptionGroup = 
                await _productOptionGroupRepository.GetByIdAsync(request.Id, cancellationToken);

            if (maybeProductOptionGroup.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            ProductOptionGroup productOptionGroup = maybeProductOptionGroup.Value;

            _productOptionGroupRepository.Remove(productOptionGroup);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
