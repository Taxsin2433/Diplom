using OrderService.Data.Interfaces;
using OrderService.Data.Models;
using OrderService.Models;
using OrderService.Services.Interfaces;
using OrderService.ViewModels;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IBasketService _basketService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IBasketService basketService,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _basketService = basketService;
            _logger = logger;
        }

        public void PlaceOrder(int userId, OrderRequestModel orderRequest)
        {
            _logger.LogInformation("Placing order for user {UserId}", userId);

            // Check if the request contains the basket information
            if (orderRequest.Basket == null || orderRequest.Basket.BasketItems == null || !orderRequest.Basket.BasketItems.Any())
            {
                _logger.LogWarning("Unable to place order. Basket is empty for user {UserId}", userId);
                return;
            }

            var order = new OrderDbModel
            {
                UserId = userId,
                Status = "Placed", // Set the initial status as "Placed"
                DateCreated = DateTime.Now,
                Items = orderRequest.Basket.BasketItems.Select(item => new OrderItemModel
                {
                    CatalogItemId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };

            _orderRepository.PlaceOrder(order);

            // Clear the basket after placing the order
            _basketService.Checkout(userId);

            _logger.LogInformation("Order placed successfully for user {UserId}, OrderId: {OrderId}", userId, order.Id);
        }

        public OrderDetailModel GetOrderDetails(int orderId)
        {
            _logger.LogInformation("Getting details for order {OrderId}", orderId);

            var order = _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                _logger.LogWarning("Order not found for OrderId: {OrderId}", orderId);
                return null;
            }

            var orderDetails = new OrderDetailModel
            {
                OrderId = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                DateCreated = order.DateCreated,
                Items = order.Items.Select(item => new OrderItemDetailModel
                {
                    CatalogId = item.CatalogItemId,
                    Quantity = item.Quantity
                }).ToList()
            };

            _logger.LogInformation("Order details retrieved successfully for OrderId: {OrderId}", orderId);

            return orderDetails;
        }
        public IEnumerable<OrderListModel> GetOrdersByUserId(int userId)
        {
            try
            {
                _logger.LogInformation("Getting orders for user {UserId}", userId);

                var orders = _orderRepository.GetOrdersByUserId(userId);

                _logger.LogInformation("Orders retrieved successfully for user {UserId}", userId);

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders for user {UserId}", userId);
                throw;
            }
        }
    }
}
