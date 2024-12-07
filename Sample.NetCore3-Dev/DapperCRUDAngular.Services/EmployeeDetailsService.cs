using DapperCRUDAngular.Abstraction.Repository;
using DapperCRUDAngular.Abstraction.Services;
using DapperCRUDAngular.Abstraction.Models;
using System.Threading.Tasks;
using System;
//using static Posta


namespace DapperCRUDAngular.Services
{

    public class EmployeeDetailsService : BaseService<EmployeeDetails>, IEmployeeDetailsService
    {
        IEmployeeDetailsRepository _EmployeeDetailsRepository;

        public EmployeeDetailsService(IEmployeeDetailsRepository EmployeeDetails)
            : base(EmployeeDetails)
        {
            _EmployeeDetailsRepository = EmployeeDetails;
        }

        public async Task<bool> UpdateEmployeeEmail(int EmpID, String EmailID)
        {
            return await _EmployeeDetailsRepository.UpdateEmployeeEmail(EmpID, EmailID);
        }

        public async Task<bool> DeleteEmployeebyid(int EmpID)
        {
            return await _EmployeeDetailsRepository.DeleteEmployeebyid(EmpID);
        }
    }
}
