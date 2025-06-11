using System;

namespace KooliProjekt.Search
{
    public class CustomerSearchParameters : BaseSearchParameters
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? MinRegistrationDate { get; set; }
        public DateTime? MaxRegistrationDate { get; set; }
    }
} 