namespace ShopUI.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
        public string ShippingAddress { get; set; }
    }
    public class OrderListModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
