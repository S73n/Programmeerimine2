using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.PublicAPI
{
    public class Batch
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string BatchNumber { get; set; }
        
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Volume { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        
        [StringLength(500)]
        public string Notes { get; set; }

        public string Summary { get; set; }
        public string BatchDescription { get; set; }

        public DateTime BatchDate { get; set; }
        public string BatchCode { get; set; }
    }
} 