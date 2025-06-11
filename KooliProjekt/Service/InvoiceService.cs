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
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesAsync(InvoiceSearchParameters searchParameters)
        {
            var query = _context.Invoices
                .Include(i => i.Customer)
                .AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(i => i.InvoiceNumber.Contains(searchParameters.SearchString) ||
                                       i.Customer.Name.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.InvoiceNumber))
            {
                query = query.Where(i => i.InvoiceNumber.Contains(searchParameters.InvoiceNumber));
            }

            if (searchParameters.CustomerId.HasValue)
            {
                query = query.Where(i => i.CustomerId == searchParameters.CustomerId.Value);
            }

            if (searchParameters.MinInvoiceDate.HasValue)
            {
                query = query.Where(i => i.IssueDate >= searchParameters.MinInvoiceDate.Value);
            }

            if (searchParameters.MaxInvoiceDate.HasValue)
            {
                query = query.Where(i => i.IssueDate <= searchParameters.MaxInvoiceDate.Value);
            }

            if (searchParameters.MinDueDate.HasValue)
            {
                query = query.Where(i => i.DueDate >= searchParameters.MinDueDate.Value);
            }

            if (searchParameters.MaxDueDate.HasValue)
            {
                query = query.Where(i => i.DueDate <= searchParameters.MaxDueDate.Value);
            }

            if (searchParameters.MinTotalAmount.HasValue)
            {
                query = query.Where(i => i.TotalAmount >= searchParameters.MinTotalAmount.Value);
            }

            if (searchParameters.MaxTotalAmount.HasValue)
            {
                query = query.Where(i => i.TotalAmount <= searchParameters.MaxTotalAmount.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Status))
            {
                query = query.Where(i => i.Status == searchParameters.Status);
            }

            // Apply sorting
            switch (searchParameters.SortColumn?.ToLower())
            {
                case "invoicenumber":
                    query = searchParameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.InvoiceNumber)
                        : query.OrderBy(i => i.InvoiceNumber);
                    break;
                case "customerid":
                    query = searchParameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.CustomerId)
                        : query.OrderBy(i => i.CustomerId);
                    break;
                case "issuedate":
                    query = searchParameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.IssueDate)
                        : query.OrderBy(i => i.IssueDate);
                    break;
                case "duedate":
                    query = searchParameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.DueDate)
                        : query.OrderBy(i => i.DueDate);
                    break;
                case "totalamount":
                    query = searchParameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.TotalAmount)
                        : query.OrderBy(i => i.TotalAmount);
                    break;
                case "status":
                    query = searchParameters.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.Status)
                        : query.OrderBy(i => i.Status);
                    break;
                default:
                    query = query.OrderBy(i => i.Id);
                    break;
            }

            // Apply paging
            return await query
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalInvoicesCountAsync(InvoiceSearchParameters searchParameters)
        {
            var query = _context.Invoices.AsQueryable();

            // Apply the same filters as in GetInvoicesAsync
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(i => i.InvoiceNumber.Contains(searchParameters.SearchString) ||
                                       i.Customer.Name.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.InvoiceNumber))
            {
                query = query.Where(i => i.InvoiceNumber.Contains(searchParameters.InvoiceNumber));
            }

            if (searchParameters.CustomerId.HasValue)
            {
                query = query.Where(i => i.CustomerId == searchParameters.CustomerId.Value);
            }

            if (searchParameters.MinInvoiceDate.HasValue)
            {
                query = query.Where(i => i.IssueDate >= searchParameters.MinInvoiceDate.Value);
            }

            if (searchParameters.MaxInvoiceDate.HasValue)
            {
                query = query.Where(i => i.IssueDate <= searchParameters.MaxInvoiceDate.Value);
            }

            if (searchParameters.MinDueDate.HasValue)
            {
                query = query.Where(i => i.DueDate >= searchParameters.MinDueDate.Value);
            }

            if (searchParameters.MaxDueDate.HasValue)
            {
                query = query.Where(i => i.DueDate <= searchParameters.MaxDueDate.Value);
            }

            if (searchParameters.MinTotalAmount.HasValue)
            {
                query = query.Where(i => i.TotalAmount >= searchParameters.MinTotalAmount.Value);
            }

            if (searchParameters.MaxTotalAmount.HasValue)
            {
                query = query.Where(i => i.TotalAmount <= searchParameters.MaxTotalAmount.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Status))
            {
                query = query.Where(i => i.Status == searchParameters.Status);
            }

            return await query.CountAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Order)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task CreateInvoiceAsync(Invoice invoice)
        {
            _context.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> InvoiceExistsAsync(int id)
        {
            return await _context.Invoices.AnyAsync(e => e.Id == id);
        }
    }
} 