using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Sandbox_Products.Utils.CrudHelpers
{
    public static class LINQ
    {
        public static IQueryable<TData> Paging<TData>(this IOrderedQueryable<TData> target, PagingModel paging, out Int32 totalPages)
        {
            Int32 total = target.Count();
            paging.Rows = paging.Rows <= 0 ? 1 : paging.Rows;
            totalPages = (total + paging.Rows - 1) / paging.Rows;

            if ((paging.Page - 1) * paging.Rows >= total)
            {
                paging.Page = (total / paging.Rows) - 1;
            }

            if (paging.Page <= 0)
            {
                paging.Page = 1;
            }

            decimal res = total / paging.Rows;           

            return target
                .Skip(paging.Rows * (paging.Page - 1))
                .Take(paging.Rows);
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, SortDirections direction)
        {
            if (direction == SortDirections.Ascending)
            {
                return source.OrderBy(keySelector);
            }
            else
            {
                return source.OrderByDescending(keySelector);
            }
        }
    }
}