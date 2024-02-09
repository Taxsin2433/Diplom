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

        public async Task<IActionResult> Index(int userId)
        {
            var response = await _apiClient.GetAsync($"/api/Bff/get-orders/{userId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<List<OrderListModel>>(content);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int userId, string shippingAddress, Dictionary<int, BasketItemRequest> basketItems)
        {
            try
            {

                var basketRequest = new BasketRequestModel
                {
                    UserId = userId,
                    BasketItems = basketItems.Values.ToList()
                };

                var orderRequest = new OrderRequestModel
                {
                    Basket = basketRequest,
                    ShippingAddress = shippingAddress
                };

                var response = await _apiClient.PostAsJsonAsync($"/api/Bff/place-order/{userId}", orderRequest);
                response.EnsureSuccessStatusCode();

                TempData["SuccessMessage"] = "Order placed successfully!";
                return RedirectToAction("Index", new { userId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
