namespace OrderService.ViewModels
{
    public class OrderItemDetailModel
    {
        public int CatalogId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
