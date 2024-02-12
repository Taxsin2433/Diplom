using BasketService.Models;

namespace OrderService.ViewModels
{
    public class BasketRequestModel
    {
        public int BasketId { get; set; }
        public int UserId { get; set; }
        public List<BasketItemRequest> BasketItems { get; set; }
    }
}
