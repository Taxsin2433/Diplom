using System.Threading.Tasks;
using BasketService.Models;

namespace OrderService.Services.Interfaces
{
    public interface IBasketService
    {
        Task Checkout(int userId);
    }
}
