using System;

namespace KooliProjekt.Search
{
    public class BeerSearchParameters : BaseSearchParameters
    {
        public string? Name { get; set; }
        public string? Style { get; set; }
        public decimal? MinAlcoholContent { get; set; }
        public decimal? MaxAlcoholContent { get; set; }
        public DateTime? MinBrewDate { get; set; }
        public DateTime? MaxBrewDate { get; set; }
        public decimal? MinSRM { get; set; }
        public decimal? MaxSRM { get; set; }
        public decimal? MinIBU { get; set; }
        public decimal? MaxIBU { get; set; }
        public string BeerName { get; set; }
    }
} 