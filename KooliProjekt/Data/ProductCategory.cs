using KooliProjekt.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class ProductCategory : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public IList<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }
    }
} 