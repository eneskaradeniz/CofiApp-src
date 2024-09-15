using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Baskets;
using CofiApp.Contracts.Products;
using CofiApp.Domain.Baskets;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Baskets.Queries.GetActiveBasketByUser
{
    public class GetActiveBasketQueryHandler :
        IQueryHandler<GetActiveBasketQuery, Maybe<BasketResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        public GetActiveBasketQueryHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Maybe<BasketResponse>> Handle(GetActiveBasketQuery request, CancellationToken cancellationToken)
        {
            BasketResponse? response = await _dbContext.Set<Basket>()
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product)
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.BasketItemOptionGroups)
                        .ThenInclude(biog => biog.ProductOptionGroup)
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.BasketItemOptionGroups)
                        .ThenInclude(biog => biog.BasketItemOptions)
                            .ThenInclude(bio => bio.ProductOption)
                .AsNoTracking()
                .Where(b => b.UserId == _userIdentifierProvider.UserId && b.Status == BasketStatus.Active)
                .Select(b => new BasketResponse
                {
                    Id = b.Id,
                    BasketItems = b.BasketItems.Select(bi => new BasketItemResponse
                    {
                        Id = bi.Id,
                        Quantity = bi.Quantity,
                        Product = new ProductForBasketItemResponse
                        {
                            Id = bi.Product.Id,
                            Name = bi.Product.Name,
                            Price = bi.Product.Price
                        },
                        BasketItemOptionGroups = bi.BasketItemOptionGroups.Select(biog => new BasketItemOptionGroupResponse
                        {
                            Id = biog.Id,
                            Name = biog.ProductOptionGroup.Name,
                            IsRequired = biog.ProductOptionGroup.IsRequired,
                            AllowMultiple = biog.ProductOptionGroup.AllowMultiple,
                            BasketItemOptions = biog.BasketItemOptions.Select(bio => new BasketItemOptionResponse
                            {
                                Id = bio.Id,
                                Name = bio.ProductOption.Name,
                                Price = bio.ProductOption.Price
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);

            return response;
        }
    }
}
