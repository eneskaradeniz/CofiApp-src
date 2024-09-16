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

        public PublicGetMenuCategoriesQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<List<PublicMenuCategoryResponse>>> Handle(PublicGetMenuCategoriesQuery request, CancellationToken cancellationToken)
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
        }
    }
}
