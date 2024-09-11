namespace CofiApp.Domain.ProductMenuCategories
{
    public interface IProductMenuCategoryRepository
    {
        Task<List<ProductMenuCategory>> GetByProductIdAsync(Guid productId);
        void Insert(ProductMenuCategory productMenuCategory);
        void RemoveRange(IReadOnlyCollection<ProductMenuCategory> productMenuCategories);
    }
}
