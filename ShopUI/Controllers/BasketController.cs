using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopUI.Models;

namespace ShopUI.Controllers
{
    public class BasketController : Controller
    {
        private readonly HttpClient _apiClient;

        public BasketController(IHttpClientFactory httpClientFactory)
        {
            _apiClient = httpClientFactory.CreateClient("BasketApi  ");
        }

        public async Task<IActionResult> Index()
        {
            var userId = 1; // Замените на ваш способ получения идентификатора пользователя

            var response = await _apiClient.GetAsync($"/api/Bff/get-basket/{userId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var basketItems = JsonConvert.DeserializeObject<List<BasketItemModel>>(content);

            return View(basketItems);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(int productId)
        {
            var userId = 1; // Замените на ваш способ получения идентификатора пользователя

            var request = new Dictionary<string, string>
        {
            { "userId", userId.ToString() },
            { "productId", productId.ToString() }
        };

            var response = await _apiClient.DeleteAsync("/api/bff/remove-item?" + new FormUrlEncodedContent(request));
            response.EnsureSuccessStatusCode();

            TempData["SuccessMessage"] = "Товар удален из корзины!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = 1; // Замените на ваш способ получения идентификатора пользователя

            var response = await _apiClient.PostAsync($"/api/bff/checkout/{userId}", null);
            response.EnsureSuccessStatusCode();

            TempData["SuccessMessage"] = "Заказ создан!";
            return RedirectToAction("Index");
        }
    }
}
