using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Persistence.Repositories
{
    internal abstract class GenericRepository<TEntity>
        where TEntity : Entity
    {
        protected GenericRepository(IDbContext dbContext) => DbContext = dbContext;

        protected IDbContext DbContext { get; }
        
        public async Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default) =>
            await DbContext.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable()
                .AnyAsync(x => x.Id == id, cancellationToken);

        public async Task<Maybe<TEntity>> GetByIdAsync(Guid id) => await DbContext.GetByIdAsync<TEntity>(id);

        public void Insert(TEntity entity) => DbContext.Insert(entity);

        public void InsertRange(IReadOnlyCollection<TEntity> entities) => DbContext.InsertRange(entities);

        public void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);

        public void Remove(TEntity entity) => DbContext.Remove(entity);

        public void RemoveRange(IReadOnlyCollection<TEntity> entities) => DbContext.Set<TEntity>().RemoveRange(entities);
    }
}
