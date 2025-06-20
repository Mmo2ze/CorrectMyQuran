﻿using Microsoft.EntityFrameworkCore;

namespace CorectMyQuran.DateBase.Common.Models
{
    public class PaginatedList<T>
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalCount = count;
            TotalPages = TotalCount > 0 ? (int)Math.Ceiling(count / (double)pageSize) : 0;
            Items = items;
        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
        public IReadOnlyCollection<T> Items { get; }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        public int GetPageSize()
        {
            return TotalCount > 0 ? HasNextPage ? Items.Count : (TotalCount / TotalPages) + 1 : 0;
        }
    }
}