using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.MenuCategories;

namespace CofiApp.Application.MenuCategories.Commands.CreateMenuCategory
{
    public class CreateMenuCategoryCommandHandler : ICommandHandler<CreateMenuCategoryCommand, Result>
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMenuCategoryCommandHandler(IMenuCategoryRepository menuCategoryRepository, IUnitOfWork unitOfWork)
        {
            _menuCategoryRepository = menuCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateMenuCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!await _menuCategoryRepository.IsNameUniqueAsync(request.Name, cancellationToken))
            {
                return Result.Failure(DomainErrors.MenuCategory.DuplicateName);
            }

            MenuCategory menuCategory = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _menuCategoryRepository.Insert(menuCategory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
