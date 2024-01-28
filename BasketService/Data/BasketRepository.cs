using BasketService.Data.Interfaces;
using BasketService.Data.Models;

namespace BasketService.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly BasketDbContext _dbContext;

        public BasketRepository(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Basket GetBasketByUserId(int userId)
        {
            return _dbContext.Baskets.SingleOrDefault(b => b.UserId == userId);
        }

        public void CreateBasket(Basket basket)
        {
            _dbContext.Baskets.Add(basket);
            _dbContext.SaveChanges();
        }

        public void UpdateBasket(Basket basket)
        {
            _dbContext.Baskets.Update(basket);
            _dbContext.SaveChanges();
        }

        public void DeleteBasket(int basketId)
        {
            var basketToRemove = _dbContext.Baskets.Find(basketId);
            if (basketToRemove != null)
            {
                _dbContext.Baskets.Remove(basketToRemove);
                _dbContext.SaveChanges();
            }
        }
    }
}
