﻿namespace Car_App.Data.Models
{
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public Boolean HasNextPage { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}