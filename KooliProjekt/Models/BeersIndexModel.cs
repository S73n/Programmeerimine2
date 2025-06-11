using System;
using System.Collections.Generic;
using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class BeersIndexModel
    {
        public BeerSearchParameters SearchParameters { get; set; } = new BeerSearchParameters();
        public IEnumerable<Beer> Beers { get; set; } = new List<Beer>();
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)SearchParameters.PageSize);
    }
} 