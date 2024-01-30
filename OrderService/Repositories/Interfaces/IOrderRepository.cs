using System.Collections.Generic;
using OrderService.Data.Models;
using OrderService.Models;

namespace OrderService.Data.Interfaces
{
    public interface IOrderRepository
    {
        void PlaceOrder(OrderDbModel order);
        OrderDbModel GetOrderById(int orderId);
        IEnumerable<OrderListModel> GetOrdersByUserId(int userId);
    }
}
