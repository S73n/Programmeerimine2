namespace KooliProjekt.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(int page, int pageSize)
        {
            return (IEnumerable<Customer>)await List(page, pageSize);
        }
    }
}
