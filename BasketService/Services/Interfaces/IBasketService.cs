using BasketService.Models;

namespace BasketService.Services.Interfaces
{
    public interface IBasketService
    {
        void AddItemToBasket(int userId, int productId, int quantity);

        void RemoveItemFromBasket(int userId, int productId);

        BasketResponse GetBasket(int userId);

        void RemoveBasket(int userId);
    }

}
