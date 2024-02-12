namespace OrderService.Data.Models
{
    public class OrderDbModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<OrderItemModel> Items { get; set; }

        public string Status { get; set; }
    }
}
