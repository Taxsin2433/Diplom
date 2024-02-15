using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopUI.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System;

public class CatalogController : Controller
{
    private readonly HttpClient _apiClientCatalog;
    private readonly HttpClient _apiClientBasket;
    private readonly IHttpClientFactory _httpClientFactory;

    public CatalogController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _apiClientCatalog = httpClientFactory.CreateClient("CatalogApi");
        _apiClientBasket = httpClientFactory.CreateClient("BasketApi");
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        // Получение JWT токена из аутентификации
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        // Добавление JWT токена в заголовок Authorization
        _apiClientCatalog.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _apiClientCatalog.GetAsync($"/api/Bff/catalogItems?page={page}&pageSize={pageSize}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var catalogItems = JsonConvert.DeserializeObject<List<CatalogItemModel>>(content);

        return View(new CatalogIndex { CatalogItems = catalogItems });
    }


    public async Task<IActionResult> Details(int id)
    {
        // Получение JWT токена из аутентификации
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        // Добавление JWT токена в заголовок Authorization
        _apiClientCatalog.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _apiClientCatalog.GetAsync($"/api/Bff/catalogItem/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var catalogItem = JsonConvert.DeserializeObject<CatalogItemDetailModel>(content);

        return View(catalogItem);
    }
}
