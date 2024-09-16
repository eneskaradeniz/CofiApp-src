using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Persistence.Repositories
{
    internal abstract class GenericRepository<TEntity>
        where TEntity : Entity
    {
        protected GenericRepository(IDbContext dbContext) => DbContext = dbContext;

        protected IDbContext DbContext { get; }

        public async Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default) =>
            await DbContext.AnyAsync<TEntity>(id, cancellationToken);

        public async Task<Maybe<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await DbContext.GetByIdAsync<TEntity>(id, cancellationToken);

        public void Insert(TEntity entity) => DbContext.Insert(entity);

        public void InsertRange(IReadOnlyCollection<TEntity> entities) => DbContext.InsertRange(entities);

        public void Update(TEntity entity) => DbContext.Update(entity);

        public void Remove(TEntity entity) => DbContext.Remove(entity);

        public void RemoveRange(IReadOnlyCollection<TEntity> entities) => DbContext.RemoveRange(entities);

        public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
            await DbContext.CountAsync<TEntity>(cancellationToken);
    }
}
