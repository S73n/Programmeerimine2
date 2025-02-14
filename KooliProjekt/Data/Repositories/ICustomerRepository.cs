namespace KooliProjekt.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync(int page, int pageSize);
    }
}
