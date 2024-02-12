namespace OrderService.ViewModels
{
    public class OrderDetailModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public List<OrderItemDetailModel> Items { get; set; }
    }
}
