namespace ShopUI.Models
{
    public class OrderDetailModel
    {

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public string ShippingAddress { get; set; }
    }

    public class OrderItemModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
