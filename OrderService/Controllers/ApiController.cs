using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderService.Models;
using OrderService.Services;
using OrderService.Services.Interfaces;
using System;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<ApiController> _logger;

        public ApiController(IOrderService orderService, ILogger<ApiController> logger)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
