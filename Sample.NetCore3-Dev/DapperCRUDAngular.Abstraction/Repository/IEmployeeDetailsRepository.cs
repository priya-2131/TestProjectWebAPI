using DapperCRUDAngular.Abstraction.Models;
using System;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Abstraction.Repository
{
    public interface IEmployeeDetailsRepository : IBaseRepository<EmployeeDetails>
    {
        Task<bool> UpdateEmployeeEmail(int EmpID,String EmailID);
        Task<bool> DeleteEmployeebyid(int EmpID);
    }
}
