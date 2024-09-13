namespace CofiApp.Domain.BasketItemOptionGroups
{
    public interface IBasketItemOptionGroupRepository
    {
        void Insert(BasketItemOptionGroup basketItemOptionGroup);
        void Update(BasketItemOptionGroup basketItemOptionGroup);
        void RemoveRange(IReadOnlyCollection<BasketItemOptionGroup> entities);
    }
}
