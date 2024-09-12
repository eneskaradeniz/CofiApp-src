using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Domain.MenuCategories
{
    public interface IMenuCategoryRepository
    {
        Task<Maybe<MenuCategory>> GetByIdAsync(Guid menuCategoryId);
        Task<Maybe<MenuCategory>> GetByNameAsync(string name);
        Task<bool> IsNameUniqueAsync(string name);
        void Insert(MenuCategory menuCategory);
        void Update(MenuCategory menuCategory);
        void Remove(MenuCategory menuCategory);
    }
}
