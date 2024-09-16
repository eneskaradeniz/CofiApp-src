using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.MenuCategories
{
    public interface IMenuCategoryRepository
    {
        Task<Maybe<MenuCategory>> GetByIdAsync(Guid menuCategoryId, CancellationToken cancellationToken = default);
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
        void Insert(MenuCategory menuCategory);
        void Update(MenuCategory menuCategory);
        void Remove(MenuCategory menuCategory);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}
