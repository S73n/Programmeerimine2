using System;
using System.Collections.Generic;
using KooliProjekt.Search;
using KooliProjekt.Data;
using KooliProjekt.Models;

namespace KooliProjekt.Models
{
    public class OrdersIndexModel
    {
        public OrderSearchParameters SearchParameters { get; set; } = new OrderSearchParameters();
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)SearchParameters.PageSize);
    }
} 