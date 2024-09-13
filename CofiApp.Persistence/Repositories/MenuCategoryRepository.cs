﻿using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.MenuCategories;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal sealed class MenuCategoryRepository : GenericRepository<MenuCategory>, IMenuCategoryRepository
    {
        public MenuCategoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Maybe<MenuCategory>> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
            await DbContext.Set<MenuCategory>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default) =>
            !await DbContext.Set<MenuCategory>()
                .AsNoTracking()
                .AsQueryable()
                .AnyAsync(x => x.Name == name, cancellationToken);
    }
}
