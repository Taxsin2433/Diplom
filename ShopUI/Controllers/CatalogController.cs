using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopUI.Models;

public class CatalogController : Controller
{
    private readonly HttpClient _apiClient;

    public CatalogController(IHttpClientFactory httpClientFactory)
    {
        _apiClient = httpClientFactory.CreateClient("CatalogApi");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _apiClient.GetAsync("/api/Bff/catalogItems");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var catalogItems = JsonConvert.DeserializeObject<List<CatalogItemModel>>(content);
        
        return View(new CatalogIndex {CatalogItems = catalogItems });

    }


    public async Task<IActionResult> Details(int id)
    {
        var response = await _apiClient.GetAsync($"/api/Bff/catalogItem/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var catalogItem = JsonConvert.DeserializeObject<CatalogItemDetailModel>(content);

        return View(catalogItem);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket(int productId, int quantity)
    {
        var userId = 1; // Замените на ваш способ получения идентификатора пользователя

        var basketItem = new BasketItemRequest
        {
            ProductId = productId,
            Quantity = quantity
        };

        var requestModel = new BasketRequestModel
        {
            UserId = userId,
            BasketItems = new List<BasketItemRequest> { basketItem }
        };

        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var httpContent = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

        var response = await _apiClient.PostAsync("/api/bff/add-item", httpContent);
        response.EnsureSuccessStatusCode();

        TempData["SuccessMessage"] = "Товар добавлен в корзину!";
        return RedirectToAction("Index");
    }
}
