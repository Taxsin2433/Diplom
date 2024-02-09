using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopUI.Models;

public class CatalogController : Controller
{
    private readonly HttpClient _apiClientCatalog;
    private readonly HttpClient _apiClientBasket;

    public CatalogController(IHttpClientFactory httpClientFactory)
    {
        _apiClientCatalog = httpClientFactory.CreateClient("CatalogApi");
        _apiClientBasket = httpClientFactory.CreateClient("BasketApi");
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var response = await _apiClientCatalog.GetAsync($"/api/Bff/catalogItems?page={page}&pageSize={pageSize}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var catalogItems = JsonConvert.DeserializeObject<List<CatalogItemModel>>(content);

        return View(new CatalogIndex { CatalogItems = catalogItems });
    }


    public async Task<IActionResult> Details(int id)
    {
        var response = await _apiClientCatalog.GetAsync($"/api/Bff/catalogItem/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var catalogItem = JsonConvert.DeserializeObject<CatalogItemDetailModel>(content);

        return View(catalogItem);
    }

   

}
