using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.MenuCategories;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.MenuCategories.Queries.GetMenuCategories
{
    public class GetMenuCategoriesQueryHandler : IQueryHandler<GetMenuCategoriesQuery, Maybe<PagedList<MenuCategoryResponse>>>
    {
        private readonly IDbContext _dbContext;

        public GetMenuCategoriesQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<PagedList<MenuCategoryResponse>>> Handle(GetMenuCategoriesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<MenuCategoryResponse> query = _dbContext.Set<MenuCategory>()
                .AsNoTracking()
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new MenuCategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOnUtc = x.CreatedOnUtc,
                    ModifiedOnUtc = x.ModifiedOnUtc,
                });

            var totalCount = await query.CountAsync(cancellationToken);

            MenuCategoryResponse[] responseArray = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new PagedList<MenuCategoryResponse>(responseArray, request.Page, request.PageSize, totalCount);
        }
    }
}
