namespace ShopUI.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
        public string ShippingAddress { get; set; }
    }

    //public class OrderDetailModel
    //{
    //    public int ProductId { get; set; }
    //    public string ProductName { get; set; }
    //    public decimal Price { get; set; }
    //    public int Quantity { get; set; }
    //}
}
