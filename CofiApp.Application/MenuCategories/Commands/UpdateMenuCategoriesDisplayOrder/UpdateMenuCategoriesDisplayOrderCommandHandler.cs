using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.MenuCategories;

namespace CofiApp.Application.MenuCategories.Commands.UpdateMenuCategoriesDisplayOrder
{
    public class UpdateMenuCategoriesDisplayOrderCommandHandler
        : ICommandHandler<UpdateMenuCategoriesDisplayOrderCommand, Result>
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMenuCategoriesDisplayOrderCommandHandler(
            IMenuCategoryRepository menuCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _menuCategoryRepository = menuCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateMenuCategoriesDisplayOrderCommand request, CancellationToken cancellationToken)
        {
            int menuCategoriesCount = await _menuCategoryRepository.CountAsync();

            if (menuCategoriesCount != request.UpdateMenuCategoriesDisplayOrders.Count)
            {
                return Result.Failure(DomainErrors.General.UnProcessableRequest);
            }

            foreach (var updateMenuCategoriesDisplayOrder in request.UpdateMenuCategoriesDisplayOrders)
            {
                Maybe<MenuCategory> maybeMenuCategory = await _menuCategoryRepository.GetByIdAsync(updateMenuCategoriesDisplayOrder.Id);

                if (maybeMenuCategory.HasNoValue)
                {
                    return Result.Failure(DomainErrors.General.NotFound);
                }

                MenuCategory menuCategory = maybeMenuCategory.Value;

                menuCategory.UpdateDisplayOrder(updateMenuCategoriesDisplayOrder.DisplayOrder);

                _menuCategoryRepository.Update(menuCategory);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
