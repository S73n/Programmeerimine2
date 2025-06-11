using System;

namespace KooliProjekt.Search
{
    public class BaseSearchParameters
    {
        public string? SearchString { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }
    }
} 