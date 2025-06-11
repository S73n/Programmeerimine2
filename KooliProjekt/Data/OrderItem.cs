using KooliProjekt.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class OrderItem : Entity
    {
        public int Id { get; set; }

        [Required]
        public Order Order { get; set; }
        public int OrderId { get; set; }

        [Required]
        public Product Product { get; set; }
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;
    }
} 