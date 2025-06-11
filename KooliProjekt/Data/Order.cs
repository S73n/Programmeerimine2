using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public class Order : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string OrderNumber { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
} 