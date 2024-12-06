using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public class Beer : Entity
    {
        public int Id { get; set; }
        public string BeerName { get; set; }
        public string BeerDescription { get; set; }
    }
}
