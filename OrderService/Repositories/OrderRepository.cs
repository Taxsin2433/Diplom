using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Interfaces;
using OrderService.Data.Models;
using OrderService.Models;

namespace OrderService.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void PlaceOrder(OrderDbModel order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public OrderDbModel GetOrderById(int orderId)
        {
            return _dbContext.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public IEnumerable<OrderListModel> GetOrdersByUserId(int userId)
        {
            return _dbContext.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderListModel
                {
                    OrderId = o.Id,
                    UserId = o.UserId,
                    Status = o.Status,
                })
                .ToList();
        }
    }
}
