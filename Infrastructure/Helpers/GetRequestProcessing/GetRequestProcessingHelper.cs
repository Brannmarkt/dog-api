using Infrastructure.Helpers.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers.GetRequestProcessing;

public static class GetRequestProcessingHelper
{
    public async static Task<IEnumerable<TEntity>> ProcessGetRequest<TEntity>(this IQueryable<TEntity> query, int pageNumber, int pageSize) where TEntity : class
    {
        return await query.Paginate(pageNumber, pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}
