﻿using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Core.Primitives.Maybe;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Abstractions.Data
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : Entity;

        Task<Maybe<TEntity>> GetByIdAsync<TEntity>(Guid id)
            where TEntity : Entity;

        void Insert<TEntity>(TEntity entity)
            where TEntity : Entity;

        void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
            where TEntity : Entity;

        void Remove<TEntity>(TEntity entity)
            where TEntity : Entity;

        Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default);
    }
}