using KooliProjekt.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Product : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public int StockQuantity { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }
    }
} 