namespace BasketService.Data.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Внешний ключ для связи с корзиной
        public int BasketId { get; set; }

        // Навигационное свойство для связи с корзиной
        public Basket Basket { get; set; }
    }
}
