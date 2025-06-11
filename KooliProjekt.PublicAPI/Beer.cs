using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.PublicAPI
{
    public class Beer
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string BeerName { get; set; }
        
        [StringLength(500)]
        public string BeerDescription { get; set; }
        
        [StringLength(50)]
        public string Style { get; set; }
        
        [Range(0, 100)]
        public decimal AlcoholContent { get; set; }
        
        [Range(0, 100)]
        public decimal IBU { get; set; }
        
        [Range(0, 100)]
        public decimal SRM { get; set; }
        
        public DateTime BrewDate { get; set; }
        
        public string Description { get; set; }
        
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Batch> Batches { get; set; } = new List<Batch>();
        public ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    }
} 