using BasketService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public ApiController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("get-basket/{userId}")]
        public IActionResult GetBasket(int userId)
        {
            var basketResponse = _basketService.GetBasket(userId);
            return Ok(basketResponse);
        }

        [HttpPost("checkout/{userId}")]
        public IActionResult Checkout(int userId)
        {

            _basketService.RemoveBasket(userId);

            return Ok("Order placed successfully");
        }
    }
}
