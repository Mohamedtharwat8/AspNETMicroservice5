using ShoppingCart.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Aggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
