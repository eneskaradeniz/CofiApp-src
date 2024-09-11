using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.MenuCategories;
using CofiApp.Domain.ProductMenuCategories;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.MenuCategories.Queries.PublicGetMenuCategoryByIdWithProducts
{
    public class PublicGetMenuCategoryByIdWithProductsQueryHandler : IQueryHandler<PublicGetMenuCategoryByIdWithProductsQuery,
        Maybe<PublicMenuCategoryWithProductsResponse>>
    {
        private readonly IDbContext _dbContext;

        public PublicGetMenuCategoryByIdWithProductsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<PublicMenuCategoryWithProductsResponse>> Handle(PublicGetMenuCategoryByIdWithProductsQuery request, CancellationToken cancellationToken)
        {
            Maybe<MenuCategory> maybeMenuCategory = await _dbContext.GetByIdAsync<MenuCategory>(request.Id);

            if (maybeMenuCategory.HasNoValue)
            {
                return Maybe<PublicMenuCategoryWithProductsResponse>.None;
            }

            MenuCategory menuCategory = maybeMenuCategory.Value;

            IQueryable<PublicProductShowcaseResponse> query = _dbContext.Set<ProductMenuCategory>()
                .AsNoTracking()
                .Where(x => x.MenuCategoryId == request.Id)
                .Include(x => x.Product)
                .Select(x => new PublicProductShowcaseResponse
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price
                });

            var totalCount = await query.CountAsync(cancellationToken);

            PublicProductShowcaseResponse[] responseArray = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new PublicMenuCategoryWithProductsResponse
            {
                Id = menuCategory.Id,
                Name = menuCategory.Name,
                Products = new PagedList<PublicProductShowcaseResponse>(responseArray, request.Page, request.PageSize, totalCount)
            };
        }
    }
}
