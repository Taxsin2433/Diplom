using BasketService.Data.Interfaces;
using BasketService.Data.Models;
using BasketService.Services.Interfaces;

namespace BasketService.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IRedisCacheService _redisCacheService;

        public BasketService(
            IBasketRepository basketRepository,
            IBasketItemRepository basketItemRepository,
            IRedisCacheService redisCacheService)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _redisCacheService = redisCacheService;
        }

        public void AddItemToBasket(int userId, int productId, int quantity)
        {
            var basket = _basketRepository.GetBasketByUserId(userId);

            if (basket == null)
            {
                basket = new Basket { UserId = userId, DateCreated = DateTime.Now };
                _basketRepository.CreateBasket(basket);
            }

            var basketItem = basket.BasketItems.FirstOrDefault(item => item.ProductId == productId);

            if (basketItem == null)
            {
                basketItem = new BasketItem { ProductId = productId, Quantity = quantity };
                basket.BasketItems.Add(basketItem);
            }
            else
            {
                basketItem.Quantity += quantity;
            }

            _basketRepository.UpdateBasket(basket);

            
            _redisCacheService.Set($"Basket:{userId}", basket);

        }

        public void RemoveItemFromBasket(int userId, int productId)
        {
            var basket = _basketRepository.GetBasketByUserId(userId);

            if (basket == null)
                return;

            var basketItem = basket.BasketItems.FirstOrDefault(item => item.ProductId == productId);

            if (basketItem != null)
            {
                basket.BasketItems.Remove(basketItem);
                _basketRepository.UpdateBasket(basket);

   
                _redisCacheService.Set($"Basket:{userId}", basket);
            }

        }

        public BasketResponse GetBasket(int userId)
        {
      
            var cachedBasket = _redisCacheService.Get<Basket>($"Basket:{userId}");
            if (cachedBasket != null)
            {
    
                return MapToBasketResponse(cachedBasket);
            }

       
            var basket = _basketRepository.GetBasketByUserId(userId);

            if (basket == null)
                return new BasketResponse();

         
            _redisCacheService.Set($"Basket:{userId}", basket);

          

            return MapToBasketResponse(basket);
        }

        public void RemoveBasket(int userId)
        {
            var basket = _basketRepository.GetBasketByUserId(userId);

            if (basket != null)
            {
                _basketRepository.DeleteBasket(basket.Id);

        
                _redisCacheService.Remove($"Basket:{userId}");

            
            }
        }

        private BasketResponse MapToBasketResponse(Basket basket)
        {
            return new BasketResponse
            {
                BasketId = basket.Id,
                UserId = basket.UserId,
                BasketItems = basket.BasketItems.Select(item => new BasketItemResponse
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }


}
