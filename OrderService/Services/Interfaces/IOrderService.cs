using OrderService.Models;
using OrderService.ViewModels;

namespace OrderService.Services.Interfaces
{
    public interface IOrderService
    {
        void PlaceOrder(int userId, OrderRequestModel orderRequest);
        OrderDetailModel GetOrderDetails(int orderId);
        IEnumerable<OrderListModel> GetOrdersByUserId(int userId);
    }
}
