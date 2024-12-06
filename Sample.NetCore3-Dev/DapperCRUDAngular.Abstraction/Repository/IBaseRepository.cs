using DapperCRUDAngular.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Abstraction.Repository
{
    public interface IBaseRepository<T> where T : Models.BaseEntity
    {
        Task<long> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T> GetAsync(int entityID);
        Task<IEnumerable<T>> GetListAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, string orderBy, Pagination pagination);
        Task<int> Count(Expression<Func<T, bool>> expression);
    }
}
