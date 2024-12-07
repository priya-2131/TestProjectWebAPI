using DapperCRUDAngular.Abstraction.Models;
using System;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Abstraction.Services
{
    public interface IEmployeeDetailsService : IBaseService<EmployeeDetails>
    {
        Task<bool> UpdateEmployeeEmail(int EmpID, String EmailID);
        Task<bool> DeleteEmployeebyid(int EmpID);
    }
}