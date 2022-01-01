using Core.Entities;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class LinqExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class, IEntity, new()
        {
            var result = CreateInstance(query, page, pageSize);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class, IEntity, new()
        {
            var result = CreateInstance(query, page, pageSize);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        private static PagedResult<T> CreateInstance<T>(this IQueryable<T> query, int page, int pageSize) where T : class, IEntity, new()
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            return result;
        }
    }
}
