using ShoppingCart.Aggregator.Models;
using System.Threading.Tasks;

namespace ShoppingCart.Aggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
