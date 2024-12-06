namespace KooliProjekt.Data.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync(int page, int pageSize);
    }
}
