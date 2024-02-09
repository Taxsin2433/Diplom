using BasketService.Services.Interfaces;
using BasketService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Controllers
{
    [ApiController]
    [Route("api/bff")]
    public class BffController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BffController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("add-item")]
        public IActionResult AddItemToBasket(BasketItemRequestModel requestModel)
        {
            _basketService.AddItemToBasket(requestModel.UserId, requestModel.ProductId, requestModel.Quantity);
            return Ok();
        }

        [HttpDelete("remove-item")]
        public IActionResult RemoveItemFromBasket(int userId, int productId)
        {
            _basketService.RemoveItemFromBasket(userId, productId);
            return Ok();
        }

        [HttpGet("get-basket/{userId}")]
        public IActionResult GetBasket(int userId)
        {
            var basketResponse = _basketService.GetBasket(userId);
            return Ok(basketResponse);
        }

        [HttpDelete("remove-basket/{userId}")]
        public IActionResult RemoveBasket(int userId)
        {
            _basketService.RemoveBasket(userId);
            return Ok();
        }
    }
}
