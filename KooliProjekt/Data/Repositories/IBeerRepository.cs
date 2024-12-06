namespace KooliProjekt.Data.Repositories
{
    public interface IBeerRepository : IRepository<Beer>
    {
        Task<IEnumerable<Beer>> GetAllBeersAsync(int page, int pageSize);
    }

    public interface IRepository<T>
    {
    }
}
