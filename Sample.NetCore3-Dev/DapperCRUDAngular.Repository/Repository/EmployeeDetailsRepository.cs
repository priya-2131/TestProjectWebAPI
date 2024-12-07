//using DapperCRUDAngular.Abstraction.Repository;
using DapperCRUDAngular.Abstraction.Models;
using Microsoft.Extensions.Configuration;
using DapperCRUDAngular.Abstraction.Repository;
using System.Threading.Tasks;
using Dapper;
using DapperCRUDAngular.Common;
using System.Data;
using System;

namespace DapperCRUDAngular.InfraStructure.Repository
{
    public class EmployeeDetailsRepository : BaseRepository<EmployeeDetails>,IEmployeeDetailsRepository
    {
        IConfiguration _appConfig;
        static bool _isTDGRepo = false;
      
        public EmployeeDetailsRepository(IConfiguration appConfig) : base(appConfig, _isTDGRepo)
        {
            _appConfig = appConfig;
           
        }

        public async Task<bool> UpdateEmployeeEmail(int EmpID, String EmailID)
        {
            using (var cn = GetConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@EmpID", EmpID);
                queryParameters.Add("@EmailID", EmailID);
                var result = await cn.QueryFirstOrDefaultAsync<bool>(Procedures.UpdateEmailID, queryParameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<bool> DeleteEmployeebyid(int EmpID)
        {
            using (var cn = GetConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@EmpID", EmpID);
                var result = await cn.QueryFirstOrDefaultAsync<bool>(Procedures.DeleteEmployeeRecord, queryParameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
