using System;

namespace KooliProjekt.Search
{
    public class IngredientSearchParameters : BaseSearchParameters
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal? MinQuantity { get; set; }
        public decimal? MaxQuantity { get; set; }
        public string? Unit { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
} 