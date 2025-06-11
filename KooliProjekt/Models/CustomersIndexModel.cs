using System;
using System.Collections.Generic;
using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class CustomersIndexModel
    {
        public CustomerSearchParameters SearchParameters { get; set; } = new CustomerSearchParameters();
        public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)SearchParameters.PageSize);
    }
} 