using MaleFashion.Models;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public string CategoryFilter { get; private set; } 

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string categoryFilter = null)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CategoryFilter = categoryFilter;
            AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, string categoryFilter = null)
        {
            var query = source;
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                
                if (typeof(T) == typeof(Product))
                {
                    query = query.Where(p => (p as Product).Category.Name == categoryFilter);
                }
            }
            var count = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize, categoryFilter);
        }
    }
}