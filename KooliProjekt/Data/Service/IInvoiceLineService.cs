using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface IInvoiceLineService
    {
        Task<IEnumerable<InvoiceLine>> GetAllInvoiceLinesAsync();
        Task<InvoiceLine> GetInvoiceLineByIdAsync(int id);
        Task<InvoiceLine> CreateInvoiceLineAsync(InvoiceLine invoiceLine);
        Task<InvoiceLine> UpdateInvoiceLineAsync(InvoiceLine invoiceLine);
        Task DeleteInvoiceLineAsync(int id);
    }
}
