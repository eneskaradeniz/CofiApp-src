using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.BasketItems;
using CofiApp.Domain.Baskets;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Baskets.Commands.ClearBasket
{
    public class ClearBasketCommandHandler : ICommandHandler<ClearBasketCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUnitOfWork _unitOfWork;

        public ClearBasketCommandHandler(IDbContext dbContext, IBasketItemRepository basketItemRepository, IUserIdentifierProvider userIdentifierProvider, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _basketItemRepository = basketItemRepository;
            _userIdentifierProvider = userIdentifierProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
        {
            Maybe<Basket> maybeBasket = await _dbContext.Set<Basket>()
                .Include(b => b.BasketItems)
                .AsNoTracking()
                .Where(b => b.UserId == _userIdentifierProvider.UserId && b.Status == BasketStatus.Active)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (maybeBasket.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            Basket basket = maybeBasket.Value;

            _basketItemRepository.RemoveRange(basket.BasketItems.ToList());

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
