using BasketService.Data.Models;

namespace BasketService.Data.Interfaces
{
    public interface IBasketRepository
    {
        Basket GetBasketByUserId(int userId);
        void CreateBasket(Basket basket);
        void UpdateBasket(Basket basket);
        void DeleteBasket(int basketId);
    }
}
