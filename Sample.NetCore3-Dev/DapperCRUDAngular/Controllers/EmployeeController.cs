using Microsoft.AspNetCore.Mvc;
using DapperCRUDAngular.Abstraction.Models;
using DapperCRUDAngular.Abstraction.Services;
using System;
using System.Threading.Tasks;

namespace PostalCodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeDetailsService _EmployeeDetailsService;
        public EmployeeController(IEmployeeDetailsService EmployeeDetailsService)
        {
            _EmployeeDetailsService = EmployeeDetailsService;
        }

        [HttpGet("GetEmployeeDetails")]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            var employeeDetails = await _EmployeeDetailsService.GetListAsync();

            return StatusCode(200, employeeDetails);

        }
        [HttpGet("GetEmployeeDetailsbyID")]
        public async Task<ActionResult<EmployeeDetails>> GetEmployeeDetailsbyID(int EmpID)
        {
            EmployeeDetails employeeDetail = await _EmployeeDetailsService.GetAsync(EmpID);

            return StatusCode(200, employeeDetail);

        }
        [HttpPost("InsertEmployeeDetails")]
        public async Task<ActionResult<long>> InsertEmployeeDetails(EmployeeDetails employeeDetails)
        {
            long employeeDetail = await _EmployeeDetailsService.AddAsync(employeeDetails);

            return StatusCode(200, employeeDetail);
        }

        [HttpPut("UpdateEmployeeDetails")]
        public async Task<ActionResult<bool>> UpdateEmployeeDetails(EmployeeDetails employeeDetails)
        {

            bool employeeDetail = await _EmployeeDetailsService.UpdateAsync(employeeDetails);

            return StatusCode(200, employeeDetail);
        }

        [HttpDelete("DeleteEmployeeDetails")]
        public async Task<ActionResult<bool>> DeleteEmployeeDetails(EmployeeDetails employeeDetails)
        {
            bool employeeDetail = await _EmployeeDetailsService.DeleteAsync(employeeDetails);

            return StatusCode(200, employeeDetail);
        }

        [HttpPut("UpdateEmployeeEmail")]
        public async Task<ActionResult<bool>> UpdateEmployeeEmail(int EmpID,String EmailID)
        {
            bool employeeDetail = await _EmployeeDetailsService.UpdateEmployeeEmail(EmpID,EmailID);

            return StatusCode(200, employeeDetail);
        }

        [HttpDelete("DeleteEmployeebyid")]
        public async Task<ActionResult<bool>> DeleteEmployeebyid(int EmpID)
        {
            bool employeeDetail = await _EmployeeDetailsService.DeleteEmployeebyid(EmpID);

            return StatusCode(200, employeeDetail);
        }

    }
}
