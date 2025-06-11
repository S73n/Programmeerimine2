using KooliProjekt.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Customer : Entity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
