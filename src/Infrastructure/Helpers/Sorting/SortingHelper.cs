using Application.Helpers;
using System.Linq.Expressions;

namespace Infrastructure.Helpers.Sorting;

public static class SortingHelper
{
    public static IQueryable<TEntity> Sort<TEntity, TProperty>(this IQueryable<TEntity> query, SortingOption sortingOption, Expression<Func<TEntity, TProperty>> expression)
    {
        return sortingOption switch
        {
            SortingOption.Ascending => query.OrderBy(expression),
            SortingOption.Descending => query.OrderByDescending(expression),
            _ => query
        };
    }
}
