namespace OrderService.Data.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CatalogItemId { get; set; }
        public int Quantity { get; set; }
        public virtual OrderDbModel Order { get; set; }
    }
}
