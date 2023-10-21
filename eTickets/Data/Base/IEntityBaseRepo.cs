using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepo<T> where T : class, IEntityBase, new()
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task RemoveAsync(int id);
    }
}
