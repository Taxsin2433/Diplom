using OrderService.Data.Interfaces;
using OrderService.Data.Models;

namespace OrderService.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderItemRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrderItem(OrderItemModel orderItem)
        {
            _dbContext.OrderItems.Add(orderItem);
            _dbContext.SaveChanges();
        }
    }
}
