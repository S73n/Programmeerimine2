using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.PublicAPI
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Quantity { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public decimal TotalCost => Price * Quantity;

        public ICollection<Beer> Beers { get; set; } = new List<Beer>();
    }
} 