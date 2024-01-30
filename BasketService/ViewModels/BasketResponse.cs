namespace BasketService.Models
{
    public class BasketResponse
    {
        public int BasketId { get; set; }
        public int UserId { get; set; }
        public List<BasketItemResponse> BasketItems { get; set; }
    }
}
