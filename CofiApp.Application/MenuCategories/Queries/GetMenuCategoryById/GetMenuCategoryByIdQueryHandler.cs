using CofiApp.Application.Abstractions.Caching;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.MenuCategories;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.MenuCategories;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.MenuCategories.Queries.GetMenuCategoryById
{
    public class GetMenuCategoryByIdQueryHandler :
        IQueryHandler<GetMenuCategoryByIdQuery, Maybe<MenuCategoryResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICacheService _cacheService;

        public GetMenuCategoryByIdQueryHandler(IDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        public async Task<Maybe<MenuCategoryResponse>> Handle(
            GetMenuCategoryByIdQuery request, CancellationToken cancellationToken) =>
            await _cacheService.GetOrCreateAsync($"MenuCategory_{request.Id}", async token =>
            {
                return await _dbContext.Set<MenuCategory>()
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new MenuCategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOnUtc = x.CreatedOnUtc,
                    ModifiedOnUtc = x.ModifiedOnUtc,
                })
                .FirstOrDefaultAsync(cancellationToken);
            }, cancellationToken: cancellationToken);
    }
}
