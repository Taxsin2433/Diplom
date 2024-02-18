using OrderService.Data.Models;

namespace OrderService.Data.Interfaces
{
    public interface IOrderItemRepository
    {
        void AddOrderItem(OrderItemModel orderItem);
    }
}
