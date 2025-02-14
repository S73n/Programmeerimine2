using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBeerRepository BeerRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IBeerRepository beerRepository,
            ICustomerRepository customerRepository,
            IInvoiceRepository invoiceRepository)
        {
            _context = context;
            BeerRepository = beerRepository;
            CustomerRepository = customerRepository;
            InvoiceRepository = invoiceRepository;
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
