using BasketService.Data.Models;

namespace BasketService.Data.Interfaces
{
    public interface IBasketItemRepository
    {
        List<BasketItem> GetBasketItemsByBasketId(int basketId);
        BasketItem GetBasketItemById(int basketItemId);
        void AddBasketItem(BasketItem basketItem);
        void UpdateBasketItem(BasketItem basketItem);
        void DeleteBasketItem(int basketItemId);
    }
}
