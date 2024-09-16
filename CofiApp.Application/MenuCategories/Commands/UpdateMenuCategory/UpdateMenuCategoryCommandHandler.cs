using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.MenuCategories;

namespace CofiApp.Application.MenuCategories.Commands.UpdateMenuCategory
{
    public class UpdateMenuCategoryCommandHandler : ICommandHandler<UpdateMenuCategoryCommand, Result>
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMenuCategoryCommandHandler(IMenuCategoryRepository menuCategoryRepository, IUnitOfWork unitOfWork)
        {
            _menuCategoryRepository = menuCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateMenuCategoryCommand request, CancellationToken cancellationToken)
        {
            var maybeMenuCategory = await _menuCategoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (maybeMenuCategory.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            MenuCategory menuCategory = maybeMenuCategory.Value;

            menuCategory.Name = request.Name;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
