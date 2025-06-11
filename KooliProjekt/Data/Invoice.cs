using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public class Invoice : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
    }
}