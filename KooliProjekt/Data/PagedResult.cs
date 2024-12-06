namespace KooliProjekt.Data
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IEnumerable<T> Items { get; set; }

        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
