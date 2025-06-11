using System;
using System.Collections.Generic;
using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class InvoicesIndexModel
    {
        public InvoiceSearchParameters SearchParameters { get; set; } = new InvoiceSearchParameters();
        public IEnumerable<Invoice> Invoices { get; set; } = new List<Invoice>();
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)SearchParameters.PageSize);
    }
} 