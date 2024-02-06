using BasketService.Data.Interfaces;
using BasketService.Data.Models;

namespace BasketService.Data
{
    public class BasketItemRepository : IBasketItemRepository
    {
        private readonly BasketDbContext _dbContext;

        public BasketItemRepository(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BasketItem> GetBasketItemsByBasketId(int basketId)
        {
            return _dbContext.BasketItems.Where(item => item.BasketId == basketId).ToList();
        }

        public BasketItem GetBasketItemById(int basketItemId)
        {
            return _dbContext.BasketItems.Find(basketItemId);
        }

        public void AddBasketItem(BasketItem basketItem)
        {
            _dbContext.BasketItems.Add(basketItem);
            _dbContext.SaveChanges();
        }

        public void UpdateBasketItem(BasketItem basketItem)
        {
            _dbContext.BasketItems.Update(basketItem);
            _dbContext.SaveChanges();
        }

        public void DeleteBasketItem(int basketItemId)
        {
            var basketItemToRemove = _dbContext.BasketItems.Find(basketItemId);
            if (basketItemToRemove != null)
            {
                _dbContext.BasketItems.Remove(basketItemToRemove);
                _dbContext.SaveChanges();
            }
        }
    }
}
