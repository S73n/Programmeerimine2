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
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(OrderSearchParameters searchParameters)
        {
            var query = _context.Orders.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(o => o.OrderNumber.Contains(searchParameters.SearchString) ||
                                       o.Customer.Name.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.OrderNumber))
            {
                query = query.Where(o => o.OrderNumber.Contains(searchParameters.OrderNumber));
            }

            if (searchParameters.CustomerId.HasValue)
            {
                query = query.Where(o => o.CustomerId == searchParameters.CustomerId.Value);
            }

            if (searchParameters.MinOrderDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= searchParameters.MinOrderDate.Value);
            }

            if (searchParameters.MaxOrderDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= searchParameters.MaxOrderDate.Value);
            }

            if (searchParameters.MinTotalAmount.HasValue)
            {
                query = query.Where(o => o.TotalAmount >= searchParameters.MinTotalAmount.Value);
            }

            if (searchParameters.MaxTotalAmount.HasValue)
            {
                query = query.Where(o => o.TotalAmount <= searchParameters.MaxTotalAmount.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Status))
            {
                query = query.Where(o => o.Status == searchParameters.Status);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(searchParameters.SortColumn))
            {
                query = searchParameters.SortOrder?.ToLower() == "desc" 
                    ? query.OrderByDescending(o => EF.Property<object>(o, searchParameters.SortColumn))
                    : query.OrderBy(o => EF.Property<object>(o, searchParameters.SortColumn));
            }

            // Apply paging
            return await query
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalOrdersCountAsync(OrderSearchParameters searchParameters)
        {
            var query = _context.Orders.AsQueryable();

            // Apply the same filters as in GetOrdersAsync
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(o => o.OrderNumber.Contains(searchParameters.SearchString) ||
                                       o.Customer.Name.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.OrderNumber))
            {
                query = query.Where(o => o.OrderNumber.Contains(searchParameters.OrderNumber));
            }

            if (searchParameters.CustomerId.HasValue)
            {
                query = query.Where(o => o.CustomerId == searchParameters.CustomerId.Value);
            }

            if (searchParameters.MinOrderDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= searchParameters.MinOrderDate.Value);
            }

            if (searchParameters.MaxOrderDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= searchParameters.MaxOrderDate.Value);
            }

            if (searchParameters.MinTotalAmount.HasValue)
            {
                query = query.Where(o => o.TotalAmount >= searchParameters.MinTotalAmount.Value);
            }

            if (searchParameters.MaxTotalAmount.HasValue)
            {
                query = query.Where(o => o.TotalAmount <= searchParameters.MaxTotalAmount.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Status))
            {
                query = query.Where(o => o.Status == searchParameters.Status);
            }

            return await query.CountAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task CreateOrderAsync(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> OrderExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(e => e.Id == id);
        }
    }
} 