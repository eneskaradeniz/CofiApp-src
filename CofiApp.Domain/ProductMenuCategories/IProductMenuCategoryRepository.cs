namespace CofiApp.Domain.ProductMenuCategories
{
    public interface IProductMenuCategoryRepository
    {
        Task<List<ProductMenuCategory>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        void Insert(ProductMenuCategory productMenuCategory);
        void RemoveRange(IReadOnlyCollection<ProductMenuCategory> productMenuCategories);
    }
}
