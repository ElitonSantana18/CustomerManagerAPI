using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Interface.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }

}
