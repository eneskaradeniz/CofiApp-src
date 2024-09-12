using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.ProductOptions;

namespace CofiApp.Application.ProductOptions.Commands.RemoveProductOption
{
    public class RemoveProductOptionCommandHandler : ICommandHandler<RemoveProductOptionCommand, Result>
    {
        private readonly IProductOptionRepository _productOptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductOptionCommandHandler(IProductOptionRepository productOptionRepository, IUnitOfWork unitOfWork)
        {
            _productOptionRepository = productOptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveProductOptionCommand request, CancellationToken cancellationToken)
        {
            Maybe<ProductOption> maybeProductOption = await _productOptionRepository.GetByIdAsync(request.Id);

            if (maybeProductOption.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            ProductOption productOption = maybeProductOption.Value;

            _productOptionRepository.Remove(productOption);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
