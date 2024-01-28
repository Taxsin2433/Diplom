namespace BasketService.Data.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }

        // Навигационное свойство для связи с элементами корзины
        public List<BasketItem> BasketItems { get; set; }
    }
}
