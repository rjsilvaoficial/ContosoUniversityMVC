using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity
{
    public class PaginatedList<T> : List<T> 
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = Convert.ToInt32(Math.Ceiling(count * 1M / pageSize));
            this.AddRange(items);
        }

        public bool HasPreviosPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var itensLista = await source.Skip(pageIndex * pageSize - pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(itensLista, count, pageIndex, pageSize);
        }
    }
}
