using CofiApp.Application.Abstractions.Caching;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.MenuCategories;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.MenuCategories.Queries.PublicGetMenuCategories
{
    public class PublicGetMenuCategoriesQueryHandler : IQueryHandler<PublicGetMenuCategoriesQuery, Maybe<List<PublicMenuCategoryResponse>>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICacheService _cacheService;

        public PublicGetMenuCategoriesQueryHandler(IDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        public async Task<Maybe<List<PublicMenuCategoryResponse>>> Handle(PublicGetMenuCategoriesQuery request, CancellationToken cancellationToken) =>
            await _cacheService.GetOrCreateAsync("PublicMenuCategories", async token =>
            {
                List<PublicMenuCategoryResponse> responseArray = await _dbContext.Set<MenuCategory>()
                .AsNoTracking()
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new PublicMenuCategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync(cancellationToken);

                return responseArray;

            }, cancellationToken: cancellationToken);
    }
}
