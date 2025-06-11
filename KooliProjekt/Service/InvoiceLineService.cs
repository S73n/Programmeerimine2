using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public class InvoiceLineService : IInvoiceLineService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvoiceLine>> GetAllInvoiceLinesAsync()
        {
            return await _context.InvoiceLines.ToListAsync();
        }

        public async Task<InvoiceLine> GetInvoiceLineByIdAsync(int id)
        {
            return await _context.InvoiceLines.FindAsync(id);
        }

        public async Task CreateInvoiceLineAsync(InvoiceLine invoiceLine)
        {
            _context.Add(invoiceLine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInvoiceLineAsync(InvoiceLine invoiceLine)
        {
            _context.Update(invoiceLine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceLineAsync(int id)
        {
            var invoiceLine = await _context.InvoiceLines.FindAsync(id);
            if (invoiceLine != null)
            {
                _context.InvoiceLines.Remove(invoiceLine);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> InvoiceLineExistsAsync(int id)
        {
            return await _context.InvoiceLines.AnyAsync(e => e.Id == id);
        }
    }
} 