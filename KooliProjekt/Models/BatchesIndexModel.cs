using System;
using System.Collections.Generic;
using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class BatchesIndexModel
    {
        public BatchSearchParameters SearchParameters { get; set; } = new BatchSearchParameters();
        public IEnumerable<Batch> Batches { get; set; } = new List<Batch>();
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)SearchParameters.PageSize);
    }
} 