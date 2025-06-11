using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Models;

namespace KooliProjekt.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(CustomerSearchParameters searchParameters)
        {
            var query = _context.Customers.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(c => c.Name.Contains(searchParameters.SearchString) ||
                                       c.Email.Contains(searchParameters.SearchString) ||
                                       c.Phone.Contains(searchParameters.SearchString) ||
                                       c.Address.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Name))
            {
                query = query.Where(c => c.Name.Contains(searchParameters.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Email))
            {
                query = query.Where(c => c.Email.Contains(searchParameters.Email));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Phone))
            {
                query = query.Where(c => c.Phone.Contains(searchParameters.Phone));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Address))
            {
                query = query.Where(c => c.Address.Contains(searchParameters.Address));
            }

            if (searchParameters.MinRegistrationDate.HasValue)
            {
                query = query.Where(c => c.RegistrationDate >= searchParameters.MinRegistrationDate.Value);
            }

            if (searchParameters.MaxRegistrationDate.HasValue)
            {
                query = query.Where(c => c.RegistrationDate <= searchParameters.MaxRegistrationDate.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(searchParameters.SortColumn))
            {
                query = searchParameters.SortOrder?.ToLower() == "desc" 
                    ? query.OrderByDescending(c => EF.Property<object>(c, searchParameters.SortColumn))
                    : query.OrderBy(c => EF.Property<object>(c, searchParameters.SortColumn));
            }

            // Apply paging
            return await query
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCustomersCountAsync(CustomerSearchParameters searchParameters)
        {
            var query = _context.Customers.AsQueryable();

            // Apply the same filters as in GetCustomersAsync
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(c => c.Name.Contains(searchParameters.SearchString) ||
                                       c.Email.Contains(searchParameters.SearchString) ||
                                       c.Phone.Contains(searchParameters.SearchString) ||
                                       c.Address.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Name))
            {
                query = query.Where(c => c.Name.Contains(searchParameters.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Email))
            {
                query = query.Where(c => c.Email.Contains(searchParameters.Email));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Phone))
            {
                query = query.Where(c => c.Phone.Contains(searchParameters.Phone));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Address))
            {
                query = query.Where(c => c.Address.Contains(searchParameters.Address));
            }

            if (searchParameters.MinRegistrationDate.HasValue)
            {
                query = query.Where(c => c.RegistrationDate >= searchParameters.MinRegistrationDate.Value);
            }

            if (searchParameters.MaxRegistrationDate.HasValue)
            {
                query = query.Where(c => c.RegistrationDate <= searchParameters.MaxRegistrationDate.Value);
            }

            return await query.CountAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CustomerExistsAsync(int id)
        {
            return await _context.Customers.AnyAsync(e => e.Id == id);
        }
    }
} 