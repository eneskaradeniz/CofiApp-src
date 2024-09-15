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
    public class GetMenuCategoryByIdWithProductsQueryHandler : IQueryHandler<GetMenuCategoryByIdWithProductsQuery,
        Maybe<MenuCategoryWithProductsResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetMenuCategoryByIdWithProductsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<MenuCategoryWithProductsResponse>> Handle(GetMenuCategoryByIdWithProductsQuery request, CancellationToken cancellationToken)
        {
            Maybe<MenuCategory> maybeMenuCategory = await _dbContext.GetByIdAsync<MenuCategory>(request.Id);

            if (maybeMenuCategory.HasNoValue)
            {
                return Maybe<MenuCategoryWithProductsResponse>.None;
            }

            MenuCategory menuCategory = maybeMenuCategory.Value;

            IQueryable<ProductShowcaseResponse> query = _dbContext.Set<ProductMenuCategory>()
                .AsNoTracking()
                .Where(x => x.MenuCategoryId == request.Id)
                .Include(x => x.Product)
                .Select(x => new ProductShowcaseResponse
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price
                });

            var totalCount = await query.CountAsync(cancellationToken);

            ProductShowcaseResponse[] responseArray = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new MenuCategoryWithProductsResponse
            {
                Id = menuCategory.Id,
                Name = menuCategory.Name,
                Products = new PagedList<ProductShowcaseResponse>(responseArray, request.Page, request.PageSize, totalCount)
            };
        }
    }
}
