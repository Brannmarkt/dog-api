using Application.Helpers;
using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync<TProperty>(QueryingOptions options, Expression<Func<T, TProperty>>? expression);
    Task<IEnumerable<T>> GetAllAsync(QueryingOptions options);
    Task<T?> GetAsync(Expression<Func<T, bool>> filter);
    Task AddAsync(T entity);
}
