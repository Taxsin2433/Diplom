using BasketService.Data.Interfaces;
using BasketService.Data.Models;
using BasketService.Models;
using BasketService.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;
using System.Text.Json;

namespace BasketService.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IDistributedCache _distributedCache;

        public BasketService(
            IBasketRepository basketRepository,
            IBasketItemRepository basketItemRepository,
            IDistributedCache distributedCache)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _distributedCache = distributedCache;
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

            // Сохранение в кеш с использованием IDistributedCache
            SetBasketToCache(userId, basket);
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

                // Сохранение в кеш с использованием IDistributedCache
                SetBasketToCache(userId, basket);
            }
        }

        public BasketResponse GetBasket(int userId)
        {
            // Попытка получения корзины из кеша
            var cachedBasket = GetBasketFromCache(userId);

            if (cachedBasket != null)
            {
                return MapToBasketResponse(cachedBasket);
            }

            // Если корзина не найдена в кеше, получаем из репозитория
            var basket = _basketRepository.GetBasketByUserId(userId);

            if (basket == null)
                return new BasketResponse();

            // Сохранение в кеш с использованием IDistributedCache
            SetBasketToCache(userId, basket);

            return MapToBasketResponse(basket);
        }

        public void RemoveBasket(int userId)
        {
            var basket = _basketRepository.GetBasketByUserId(userId);

            if (basket != null)
            {
                _basketRepository.DeleteBasket(basket.Id);

                // Удаление из кеша с использованием IDistributedCache
                RemoveBasketFromCache(userId);
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

        private void SetBasketToCache(int userId, Basket basket)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Установите желаемое время истечения
            };

            var serializedBasket = JsonSerializer.Serialize(basket);
            _distributedCache.SetString($"Basket:{userId}", serializedBasket, options);
        }

        private Basket GetBasketFromCache(int userId)
        {
            var cachedBasket = _distributedCache.GetString($"Basket:{userId}");

            if (cachedBasket != null)
            {
                return JsonSerializer.Deserialize<Basket>(cachedBasket);
            }

            return null;
        }

        private void RemoveBasketFromCache(int userId)
        {
            _distributedCache.Remove($"Basket:{userId}");
        }
    }
}
