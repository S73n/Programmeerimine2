namespace KooliProjekt.Data.Repositories
{
    public class BeerRepository : BaseRepository<Beer>, IBeerRepository
    {
        public BeerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Beer>> GetAllBeersAsync(int page, int pageSize)
        {
            var pagedResult = await List(page, pageSize);
            return pagedResult.Items;
        }
    }
}
