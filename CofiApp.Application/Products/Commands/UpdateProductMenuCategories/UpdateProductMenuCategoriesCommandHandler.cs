using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.ProductMenuCategories;
using CofiApp.Domain.Products;

namespace CofiApp.Application.Products.Commands.UpdateProductMenuCategories
{
    public class UpdateProductMenuCategoriesCommandHandler : 
        ICommandHandler<UpdateProductMenuCategoriesCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductMenuCategoryRepository _productMenuCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductMenuCategoriesCommandHandler(IProductMenuCategoryRepository productMenuCategoryRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _productMenuCategoryRepository = productMenuCategoryRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(UpdateProductMenuCategoriesCommand request, CancellationToken cancellationToken)
        {
            if (!await _productRepository.AnyAsync(request.ProductId, cancellationToken))
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            List<ProductMenuCategory> productMenuCategories = 
                await _productMenuCategoryRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

            _productMenuCategoryRepository.RemoveRange(productMenuCategories);

            foreach (var menuCategoryId in request.MenuCategoryIds)
            {
                var productMenuCategory = new ProductMenuCategory
                {
                    ProductId = request.ProductId,
                    MenuCategoryId = menuCategoryId
                };

                _productMenuCategoryRepository.Insert(productMenuCategory);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
