using System;

namespace KooliProjekt.Search
{
    public class InvoiceSearchParameters : BaseSearchParameters
    {
        public string? InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? MinInvoiceDate { get; set; }
        public DateTime? MaxInvoiceDate { get; set; }
        public decimal? MinTotalAmount { get; set; }
        public decimal? MaxTotalAmount { get; set; }
        public string? Status { get; set; }
        public DateTime? MinDueDate { get; set; }
        public DateTime? MaxDueDate { get; set; }
    }
} 