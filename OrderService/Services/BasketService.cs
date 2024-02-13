using OrderService.Services.Interfaces;

namespace OrderService.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Checkout(int userId)
        {
            var response = await _httpClient.PostAsync($"api/checkout/{userId}", null);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
              
            }
        }
    }
}
