using System;
using System.Collections.Generic;
using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class IngredientsIndexModel
    {
        public IngredientSearchParameters SearchParameters { get; set; } = new IngredientSearchParameters();
        public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)SearchParameters.PageSize);
    }
} 