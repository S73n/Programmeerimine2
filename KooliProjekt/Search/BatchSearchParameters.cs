using System;

namespace KooliProjekt.Search
{
    public class BatchSearchParameters : BaseSearchParameters
    {
        public string? BatchNumber { get; set; }
        public int? BeerId { get; set; }
        public DateTime? MinStartDate { get; set; }
        public DateTime? MaxStartDate { get; set; }
        public DateTime? MinEndDate { get; set; }
        public DateTime? MaxEndDate { get; set; }
        public decimal? MinVolume { get; set; }
        public decimal? MaxVolume { get; set; }
        public string? Status { get; set; }
    }
} 