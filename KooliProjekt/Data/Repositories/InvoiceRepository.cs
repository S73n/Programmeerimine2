namespace KooliProjekt.Data.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync(int page, int pageSize)
        {
            return (IEnumerable<Invoice>)await List(page, pageSize);
        }
    }
}
