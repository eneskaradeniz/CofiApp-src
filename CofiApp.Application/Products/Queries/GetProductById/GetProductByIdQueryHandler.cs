using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Maybe<ProductResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetProductByIdQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            ProductResponse? response = await _dbContext.Set<Product>()
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new ProductResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CreatedOnUtc = x.CreatedOnUtc,
                    ModifiedOnUtc = x.ModifiedOnUtc
                })
                .FirstOrDefaultAsync(cancellationToken);

            return response;
        }
    }
}
