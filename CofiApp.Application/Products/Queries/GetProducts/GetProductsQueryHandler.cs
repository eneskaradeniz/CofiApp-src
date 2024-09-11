using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Common;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Maybe<PagedList<ProductResponse>>>
    {
        private readonly IDbContext _dbContext;

        public GetProductsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<PagedList<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProductResponse> query = _dbContext.Set<Product>()
                .AsNoTracking()
                .Select(x => new ProductResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CreatedOnUtc = x.CreatedOnUtc,
                    ModifiedOnUtc = x.ModifiedOnUtc
                });

            var totalCount = await query.CountAsync(cancellationToken);

            ProductResponse[] responseArray = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new PagedList<ProductResponse>(responseArray, request.Page, request.PageSize, totalCount);
        }
    }
}
