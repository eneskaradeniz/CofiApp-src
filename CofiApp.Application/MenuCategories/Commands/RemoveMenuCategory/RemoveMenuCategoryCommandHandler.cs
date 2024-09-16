using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.MenuCategories;

namespace CofiApp.Application.MenuCategories.Commands.RemoveMenuCategory
{
    public class RemoveMenuCategoryCommandHandler : ICommandHandler<RemoveMenuCategoryCommand, Result>
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveMenuCategoryCommandHandler(IMenuCategoryRepository menuCategoryRepository, IUnitOfWork unitOfWork)
        {
            _menuCategoryRepository = menuCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveMenuCategoryCommand request, CancellationToken cancellationToken)
        {
            var maybeMenuCategory = await _menuCategoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (maybeMenuCategory.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            MenuCategory menuCategory = maybeMenuCategory.Value;

            _menuCategoryRepository.Remove(menuCategory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
