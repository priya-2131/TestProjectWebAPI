using DapperCRUDAngular.Abstraction.Models;
using DapperCRUDAngular.Abstraction.Repository;
using DapperCRUDAngular.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected IBaseRepository<T> _repo;
        public BaseService(IBaseRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual async Task<long> AddAsync(T entity)
        {
            return await _repo.AddAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            return await _repo.DeleteAsync(entity);
        }

        public virtual async Task<T> GetAsync(int entityID)
        {
            return await _repo.GetAsync(entityID);
        }

        public virtual async Task<IEnumerable<T>> GetListAsync()
        {
            return await _repo.GetListAsync();
        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            return await _repo.UpdateAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAll(string orderBy, Pagination pagination)
        {
            return await _repo.GetAllAsync(null, orderBy, pagination);
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression, string orderBy, Pagination pagination)
        {
            return await _repo.GetAllAsync(expression, orderBy, pagination);
        }

        public virtual async Task<int> Count(Expression<Func<T, bool>> expression)
        {
            return await _repo.Count(expression);
        }
    }
}
