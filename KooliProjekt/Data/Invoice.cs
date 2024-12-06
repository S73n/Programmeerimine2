using KooliProjekt.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Invoice : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public IList<InvoiceLine> Lines { get; set; }

        public Invoice()
        {
            Lines = new List<InvoiceLine>();
        }
    }
}