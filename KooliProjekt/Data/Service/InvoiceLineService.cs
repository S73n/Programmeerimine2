using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.InvoiceLines.Include(il => il.Beer).ToListAsync();
        }

        public async Task<InvoiceLine> GetInvoiceLineByIdAsync(int id)
        {
            return await _context.InvoiceLines.Include(il => il.Beer)
                                              .FirstOrDefaultAsync(il => il.Id == id);
        }

        public async Task<InvoiceLine> CreateInvoiceLineAsync(InvoiceLine invoiceLine)
        {
            _context.InvoiceLines.Add(invoiceLine);
            await _context.SaveChangesAsync();
            return invoiceLine;
        }

        public async Task<InvoiceLine> UpdateInvoiceLineAsync(InvoiceLine invoiceLine)
        {
            _context.InvoiceLines.Update(invoiceLine);
            await _context.SaveChangesAsync();
            return invoiceLine;
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
    }
}
