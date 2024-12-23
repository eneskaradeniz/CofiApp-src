﻿using Microsoft.EntityFrameworkCore.Storage;

namespace CofiApp.Application.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
