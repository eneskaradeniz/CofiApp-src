using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.ProductOptionGroups;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.ProductOptionGroups;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.ProductOptionGroups.Queries.GetProductOptionGroupsWithOptions
{
    public class GetProductOptionGroupsWithOptionsQueryHandler :
        IQueryHandler<GetProductOptionGroupsWithOptionsQuery, Maybe<List<ProductOptionGroupsWithOptionsResponse>>>
    {
        private readonly IDbContext _dbContext;

        public GetProductOptionGroupsWithOptionsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<List<ProductOptionGroupsWithOptionsResponse>>> Handle(GetProductOptionGroupsWithOptionsQuery request, CancellationToken cancellationToken)
        {
            List<ProductOptionGroupsWithOptionsResponse> response = await _dbContext.Set<ProductOptionGroup>()
                .AsNoTracking()
                .Include(pog => pog.ProductOptions)
                .Where(pog => pog.ProductId == request.ProductId)
                .Select(pog => new ProductOptionGroupsWithOptionsResponse
                {
                    Id = pog.Id,
                    Name = pog.Name,
                    IsRequired = pog.IsRequired,
                    AllowMultiple = pog.AllowMultiple,
                    CreatedOnUtc = pog.CreatedOnUtc,
                    ModifiedOnUtc = pog.ModifiedOnUtc,
                    Options = pog.ProductOptions.Select(po => new ProductOptionResponse
                    {
                        Id = po.Id,
                        Name = po.Name,
                        Price = po.Price,
                        CreatedOnUtc = po.CreatedOnUtc,
                        ModifiedOnUtc = po.ModifiedOnUtc
                    })
                }).ToListAsync(cancellationToken);

            return response;
        }
    }
}
