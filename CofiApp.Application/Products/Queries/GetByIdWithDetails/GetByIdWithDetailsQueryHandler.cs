using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.ProductOptions;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Products.Queries.PublicGetProductById
{
    public class GetByIdWithDetailsQueryHandler :
        IQueryHandler<GetByIdWithDetailsQuery, Maybe<ProductWithDetailsResponse>>
    {
        private readonly IDbContext _dbContext;

        public GetByIdWithDetailsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<ProductWithDetailsResponse>> Handle(GetByIdWithDetailsQuery request, CancellationToken cancellationToken)
        {
            ProductWithDetailsResponse? response = await _dbContext.Set<Product>()
                .AsNoTracking()
                .Include(x => x.ProductOptionGroups)
                .ThenInclude(x => x.ProductOptions)
                .Where(x => x.Id == request.Id)
                .Select(x => new ProductWithDetailsResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ProductOptionGroups = x.ProductOptionGroups.Select(y => new ProductOptionGroupResponse
                    {
                        Id = y.Id,
                        Name = y.Name,
                        IsRequired = y.IsRequired,
                        AllowMultiple = y.AllowMultiple,
                        Options = y.ProductOptions.Select(z => new ProductOptionResponse 
                        { 
                            Id = z.Id,
                            Name = z.Name,
                            Price = z.Price
                        }).ToList()
                    }).ToList(),
                })
                .FirstOrDefaultAsync(cancellationToken);

            return response;
        }
    }
}
