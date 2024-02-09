namespace ShopUI.Models
{
    public class OrderRequestModel
    {
        public BasketRequestModel Basket { get; set; }
        public string ShippingAddress { get; set; }
    }

    public class BasketRequestModel
    {
        public int BasketId { get; set; }
        public int UserId { get; set; }
        public List<BasketItemRequest> BasketItems { get; set; }
    }

    public class BasketItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

