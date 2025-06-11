using System;

namespace KooliProjekt.Search
{
    public class OrderSearchParameters : BaseSearchParameters
    {
        public string? OrderNumber { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? MinOrderDate { get; set; }
        public DateTime? MaxOrderDate { get; set; }
        public decimal? MinTotalAmount { get; set; }
        public decimal? MaxTotalAmount { get; set; }
        public string? Status { get; set; }
    }
} 