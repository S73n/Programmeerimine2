using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(OrderSearchParameters searchParameters);
        Task<int> GetTotalOrdersCountAsync(OrderSearchParameters searchParameters);
        Task<Order> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task<bool> OrderExistsAsync(int id);
    }
} 