using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public class Customer : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
