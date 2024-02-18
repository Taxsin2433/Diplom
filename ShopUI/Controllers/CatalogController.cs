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

        if (string.IsNullOrEmpty(accessToken))
        {
            // Редирект на страницу входа, если токен отсутствует
            return RedirectToAction("Login", "Account");
        }

        // Создание экземпляра HttpClient
        using (var httpClient = new HttpClient())
        {
            // Добавление JWT токена в заголовок Authorization
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Выполнение HTTP-запроса к вашему API
            var response = await httpClient.GetAsync($"http://localhost:5105/api/Bff/catalogItems?page={page}&pageSize={pageSize}");

            // Проверка успешности выполнения запроса
            if (response.IsSuccessStatusCode)
            {
                // Чтение ответа от API
                var content = await response.Content.ReadAsStringAsync();
                var catalogItems = JsonConvert.DeserializeObject<List<CatalogItemModel>>(content);

                // Возврат представления с полученными данными
                return View(new CatalogIndex { CatalogItems = catalogItems });
            }
            else
            {
                // В случае ошибки, обработка ошибки или вывод сообщения об ошибке
                return View("Error");
            }
        }
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
