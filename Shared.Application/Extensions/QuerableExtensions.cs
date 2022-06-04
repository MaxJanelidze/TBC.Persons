using Shared.Application.PagedList;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Shared.Application.Extensions
{
    public static class QuerableExtensions
    {
        public static IQueryable<TSource> SortAndPage<TSource>(this IQueryable<TSource> source, SortedAndPagedListRequestBase request)
            where TSource : class
        {
            return Page(source.Sort(request), request);
        }

        public static IQueryable<TSource> Sort<TSource>(this IQueryable<TSource> source, SortedAndPagedListRequestBase request)
            where TSource : class
        {
            if (request != null && !string.IsNullOrWhiteSpace(request.SortBy))
            {
                return source.Sort(request.SortBy);
            }

            return source;
        }

        public static IQueryable<TSource> Sort<TSource>(this IQueryable<TSource> source, string orderBy)
            where TSource : class
        {
            string sort;

            if (!string.IsNullOrWhiteSpace(sort = string.Format("{0}", orderBy)))
            {
                var orderedSoruce = source.AsQueryable().OrderBy(sort);

                return orderedSoruce;
            }

            return source;
        }
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, SortedAndPagedListRequestBase request)
                where TSource : class
        {
            return source.Skip(request.PageSize * (request.CurrentPage - 1)).Take(request.PageSize);
        }

        public static IQueryable<TSource> AndIf<TSource, TFilter>(this IQueryable<TSource> source, TFilter? filter, Expression<Func<TSource, bool>> predicate)
            where TFilter : struct
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (filter.HasValue)
            {
                return source.And(predicate);
            }

            return source;
        }

        public static IQueryable<TSource> AndIf<TSource>(this IQueryable<TSource> source, string? filter, Expression<Func<TSource, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                return source.And(predicate);
            }

            return source;
        }

        public static IQueryable<TSource> AndIf<TSource>(this IQueryable<TSource> source, bool filter, Expression<Func<TSource, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (filter)
            {
                return source.And(predicate);
            }

            return source;
        }

        public static IQueryable<TSource> AndIf<TSource>(this IQueryable<TSource> source, IEnumerable? input, Expression<Func<TSource, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (input != null && input.GetEnumerator().MoveNext())
            {
                return source.And(predicate);
            }

            return source;
        }

        public static IQueryable<TSource> And<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return source.Where(predicate);
        }
    }
}
