using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopUI.Models;

namespace ShopUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _apiClient;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _apiClient = httpClientFactory.CreateClient("OrderApi");
        }

        public async Task<IActionResult> Index(int orderId)
        {
            var response = await _apiClient.GetAsync($"/api/Bff/get-order/{orderId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<OrderModel>(content);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string shippingAddress)
        {
            var userId = 1; // Замените на ваш способ получения идентификатора пользователя

            var basketRequest = new BasketRequestModel
            {
                UserId = userId,
                BasketItems = new List<BasketItemRequest>() // Здесь добавьте товары из корзины пользователя
            };

            var orderRequest = new OrderRequestModel
            {
                Basket = basketRequest,
                ShippingAddress = shippingAddress
            };

            var response = await _apiClient.PostAsJsonAsync($"/api/Bff/place-order/{userId}", orderRequest);
            response.EnsureSuccessStatusCode();

            var orderId = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());

            TempData["SuccessMessage"] = "Заказ успешно размещен!";
            return RedirectToAction("Index", new { orderId });
        }
    }
}
