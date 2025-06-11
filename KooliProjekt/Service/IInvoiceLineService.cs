using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface IInvoiceLineService
    {
        Task<IEnumerable<InvoiceLine>> GetAllInvoiceLinesAsync();
        Task<InvoiceLine> GetInvoiceLineByIdAsync(int id);
        Task CreateInvoiceLineAsync(InvoiceLine invoiceLine);
        Task UpdateInvoiceLineAsync(InvoiceLine invoiceLine);
        Task DeleteInvoiceLineAsync(int id);
        Task<bool> InvoiceLineExistsAsync(int id);
    }
} 