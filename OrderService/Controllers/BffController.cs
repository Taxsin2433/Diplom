using Microsoft.AspNetCore.Mvc;
using OrderService.Services.Interfaces;
using OrderService.ViewModels;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BffController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<BffController> _logger;

        public BffController(IOrderService orderService, ILogger<BffController> logger)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("place-order/{userId}")]
        public IActionResult PlaceOrder(int userId, [FromBody] OrderRequestModel orderRequest)
        {
            try
            {
                _logger.LogInformation("Placing order for user {UserId}", userId);

                _orderService.PlaceOrder(userId, orderRequest);

                return Ok("Order placed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error placing order for user {UserId}", userId);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("get-order/{orderId}")]
        public IActionResult GetOrder(int orderId)
        {
            try
            {
                _logger.LogInformation("Getting order details for OrderId: {OrderId}", orderId);

                var orderDetails = _orderService.GetOrderDetails(orderId);

                if (orderDetails == null)
                {
                    _logger.LogWarning("Order details not found for OrderId: {OrderId}", orderId);
                    return NotFound("Order details not found");
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order details for OrderId: {OrderId}", orderId);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
