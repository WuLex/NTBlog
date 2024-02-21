using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTBlogWeb.Core.Extension
{
    public static class QueryExt
    {
        public static IQueryable<TSource> HasWhere<TSource>(this IQueryable<TSource> query, object target,
            Expression<Func<TSource, bool>> whExpression)
        {
            if (target != null)
            {
                query = query.Where(whExpression);
            }

            return query;
        }

        public static IQueryable<TSource> HasWhere<TSource>(this IQueryable<TSource> query, object target,
            Expression<Func<TSource, int, bool>> whExpression)
        {
            if (target != null)
            {
                query = query.Where(whExpression);
            }

            return query;
        }


        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageIndex, int pageSize,
            bool isOrderBy = false) where T : BaseEntity
        {
            if (!isOrderBy)
                query = query.OrderBy(p => p.Id);

            var page = new PagedList<T>();
            var totalItems = query.Count();
            var totalPages = (totalItems % pageSize) == 0 ? (totalItems / pageSize) : (totalItems / pageSize) + 1;
            page.CurrentPage = pageIndex;
            page.ItemsPerPage = pageSize;
            page.TotalItems = totalItems;
            page.TotalPages = totalPages;
            page.Items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return page;
        }
    }
}