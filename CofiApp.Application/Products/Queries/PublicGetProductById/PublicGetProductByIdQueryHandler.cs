using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Products.Queries.PublicGetProductById
{
    public class PublicGetProductByIdQueryHandler :
        IQueryHandler<PublicGetProductByIdQuery, Maybe<PublicProductWithOptionsResponse>>
    {
        private readonly IDbContext _dbContext;

        public PublicGetProductByIdQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<PublicProductWithOptionsResponse>> Handle(PublicGetProductByIdQuery request, CancellationToken cancellationToken)
        {
            PublicProductWithOptionsResponse? response = await _dbContext.Set<Product>()
                .AsNoTracking()
                .Include(x => x.ProductOptionGroups)
                .ThenInclude(x => x.ProductOptions)
                .Where(x => x.Id == request.Id)
                .Select(x => new PublicProductWithOptionsResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ProductOptionGroups = x.ProductOptionGroups.Select(y => new PublicProductOptionGroupResponse
                    {
                        Id = y.Id,
                        Name = y.Name,
                        IsRequired = y.IsRequired,
                        AllowMultiple = y.AllowMultiple,
                        Options = y.ProductOptions.Select(z => new PublicProductOptionResponse 
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
