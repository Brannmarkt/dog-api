using Application.Helpers;
using Application.Interfaces;
using Infrastructure.Context;
using Infrastructure.Helpers.GetRequestProcessing;
using Infrastructure.Helpers.Sorting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(ApiDbContext context) : IGenericRepository<T> where T : class
{
    private DbSet<T> dbSet = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync<TProperty>(QueryingOptions options, Expression<Func<T, TProperty>>? expression = null)
    {
        IQueryable<T> query = dbSet.AsQueryable();
        if(expression is not null)
        {
            query = query.Sort(options.SortingOption, expression);
        }
        return await query.ProcessGetRequest(options.PageNumber, options.PageSize);
    }

    public async Task<IEnumerable<T>> GetAllAsync(QueryingOptions options)
    {   
        return await dbSet.ProcessGetRequest(options.PageNumber, options.PageSize);
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await dbSet.FirstOrDefaultAsync(filter);
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }
}
