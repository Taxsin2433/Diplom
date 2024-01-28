namespace BasketService.Data.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }


        public List<BasketItem> BasketItems { get; set; }
    }
}
