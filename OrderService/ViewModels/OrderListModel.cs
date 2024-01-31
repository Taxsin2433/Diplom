namespace OrderService.Models
{
    public class OrderListModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
