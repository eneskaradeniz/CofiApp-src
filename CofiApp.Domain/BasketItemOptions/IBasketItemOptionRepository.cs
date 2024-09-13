namespace CofiApp.Domain.BasketItemOptions
{
    public interface IBasketItemOptionRepository
    {
        void Insert(BasketItemOption basketItemOption);
        void Update(BasketItemOption basketItemOption);
        void RemoveRange(IReadOnlyCollection<BasketItemOption> entities);
    }
}
