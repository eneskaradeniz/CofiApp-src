using CofiApp.Application.Abstractions.Common;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Persistence.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace CofiApp.Persistence
{
    public sealed class ApplicationDbContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly IDateTime _dateTime;
        private readonly IDbSeedData _dbSeedData;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime, IDbSeedData dbSeedData) : base(options)
        {
            _dateTime = dateTime;
            _dbSeedData = dbSeedData;
        }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyUtcDateTimeConverter();

            await _dbSeedData.RunAsync(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void UpdateAuditableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;
                }
            }
        }

        private void UpdateSoftDeletableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
            {
                if (entityEntry.State != EntityState.Deleted)
                {
                    continue;
                }

                entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = utcNow;

                entityEntry.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

                entityEntry.State = EntityState.Modified;

                UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
            }
        }

        private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
        {
            if (!entityEntry.References.Any())
            {
                return;
            }

            foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry.State == EntityState.Deleted))
            {
                referenceEntry.TargetEntry.State = EntityState.Unchanged;

                UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
            }
        }

        #region IUnitOfWork

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime utcNow = _dateTime.UtcNow;

            UpdateAuditableEntities(utcNow);

            UpdateSoftDeletableEntities(utcNow);

            return await base.SaveChangesAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => Database.BeginTransactionAsync(cancellationToken);

        #endregion

        #region IDbContext

        public new DbSet<TEntity> Set<TEntity>()
           where TEntity : Entity
            => base.Set<TEntity>();

        public async Task<bool> AnyAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
            where TEntity : Entity
            => await Set<TEntity>()
                .AsNoTracking()
                .AsQueryable()
                .AnyAsync(x => x.Id == id, cancellationToken);

        public async Task<Maybe<TEntity>> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
            where TEntity : Entity
            => id == Guid.Empty ?
                Maybe<TEntity>.None :
                Maybe<TEntity>.From(await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken));

        public void Insert<TEntity>(TEntity entity)
            where TEntity : Entity
            => Set<TEntity>().Add(entity);

        public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
            where TEntity : Entity
            => Set<TEntity>().AddRange(entities);

        public new void Update<TEntity>(TEntity entity)
            where TEntity : Entity
            => Set<TEntity>().Update(entity);

        public new void Remove<TEntity>(TEntity entity)
            where TEntity : Entity
            => Set<TEntity>().Remove(entity);

        public void RemoveRange<TEntity>(IReadOnlyCollection<TEntity> entities)
            where TEntity : Entity 
            => Set<TEntity>().RemoveRange(entities);

        public async Task<int> CountAsync<TEntity>(CancellationToken cancellationToken = default) 
            where TEntity : Entity 
            => await Set<TEntity>().CountAsync(cancellationToken);

        public async Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default)
            => await Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);

        #endregion
    }
}
