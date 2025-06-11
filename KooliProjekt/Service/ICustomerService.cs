using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(CustomerSearchParameters searchParameters);
        Task<int> GetTotalCustomersCountAsync(CustomerSearchParameters searchParameters);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<bool> CustomerExistsAsync(int id);
    }
} 