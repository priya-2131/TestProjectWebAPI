using DapperCRUDAngular.Abstraction.Models;
using DapperCRUDAngular.Abstraction.Repository;
using DapperCRUDAngular.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DapperCRUDAngular.InfraStructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        IConfiguration appConfig;
        bool _isTDGRepo;
        public BaseRepository(IConfiguration config, bool isTDGRepo)
        {
            appConfig = config;
            _isTDGRepo = isTDGRepo;
        }

        protected IDbConnection GetConnection()
        {
            if(_isTDGRepo)
            return new SqlConnection(appConfig.GetConnectionString("TDGDbConnection"));
            else
            return new SqlConnection(appConfig.GetConnectionString("AngularCRUDDbConnection"));           
        }

        public virtual async Task<long> AddAsync(T entity)
        {
            using (var cn = GetConnection())
            {
                return await cn.InsertAsync(entity);
            }
        }       

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            using (var cn = GetConnection())
            {
                return await cn.DeleteAsync(entity);
            }
        }

        public virtual async Task<T> GetAsync(int entityID)
        {
            using (var cn = GetConnection())
            {
                return await cn.GetAsync<T>(entityID);
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            using (var cn = GetConnection())
            {
                return await cn.UpdateAsync(entity);
            }
        }

        public virtual async Task<IEnumerable<T>> GetListAsync()
        {
            using (var cn = GetConnection())
            {
                return await cn.GetAllAsync<T>();
            }
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, string orderBy, Pagination pagination)
        {
            pagination = pagination ?? new Pagination();
            

            using (var cn = GetConnection())
            {
                string whereClause = expression == null ? "" : $"WHERE {expression.ToSql()}";
                string orderByClause = String.IsNullOrWhiteSpace(orderBy) ? "" : $"ORDER BY {orderBy}";
                T firstElement = null;
                IEnumerable<T> result = null;
               
                result = await cn.QueryAsync<T>($"SELECT *, COUNT(*) OVER() as TotalRecords FROM {"tbl" + typeof(T).Name} {whereClause} {orderByClause} {CommonUtils.PagingQueryClause(pagination.PageIndex, pagination.PageSize)}");
                //result = await cn.QueryAsync<T>($"SELECT *, COUNT(*) OVER() as TotalRecords FROM {"tbl" + typeof(T).Name } {whereClause} {orderByClause}");
                firstElement = result.FirstOrDefault();
                                                           
                if (firstElement != null)
                {
                    pagination.TotalRecords = firstElement.TotalRecords;
                    pagination.TotalPages = (firstElement.TotalRecords / pagination.PageSize) + (firstElement.TotalRecords % pagination.PageSize > 0 ? 1 : 0);
                }

                return result;
            }
        }

        public virtual async Task<int> Count(Expression<Func<T, bool>> expression)
        {
            using (var cn = GetConnection())
            {
                return await cn.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM {typeof(T).Name} WHERE {expression.ToSql()}");
            }
        }
    }
}
