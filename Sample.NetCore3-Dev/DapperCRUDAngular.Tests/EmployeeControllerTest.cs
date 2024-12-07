using DapperCRUDAngular.Abstraction.Models;
using DapperCRUDAngular.Abstraction.Services;
using Moq;
using PostalCodeAPI.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DapperCRUDAngular.Tests
{
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployeeDetailsService> _mockEmployeeDetailsService;
        public EmployeeControllerTest()
        {
            _mockEmployeeDetailsService = new Mock<IEmployeeDetailsService>();
        }

        [Fact]
        public async Task GetEmployeeDetails_Success_ExpectedBehavior()
        {
            //Arrange
            List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
            employeeDetails.Add(new EmployeeDetails
            {
                EmpID = 10001,
                EmpName = "Rajat Sharma",
                EmailID = "Rajat@kcsitglobal.com",
                Gender = "Male",
                EmpAddress = "Gujarat University",
                Pincode = "382144",
                DateOfBirth = "12/12/2022"
            });

            // Arrange & Act
            _mockEmployeeDetailsService.Setup(x => x.GetListAsync()).ReturnsAsync(employeeDetails);
            var employeeController = new EmployeeController(_mockEmployeeDetailsService.Object);
            var result = await employeeController.GetEmployeeDetails();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEmployeeDetailsID_Success_ExpectedBehavior()
        {
            //Arrange
            EmployeeDetails employeeDetails = new EmployeeDetails()
            {
                EmpID = 10001,
                EmpName = "Rajat Sharma",
                EmailID = "Rajat@kcsitglobal.com",
                Gender = "Male",
                EmpAddress = "Gujarat University",
                Pincode = "382144",
                DateOfBirth = "12/12/2022"
            };

            // Arrange & Act
            _mockEmployeeDetailsService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(employeeDetails);
            var employeeController = new EmployeeController(_mockEmployeeDetailsService.Object);
            var result = await employeeController.GetEmployeeDetailsbyID(10001);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task InsertEmployeeDetails_Success_ExpectedBehavior()
        {
            //Arrange
            EmployeeDetails employeeDetails = new EmployeeDetails()
            {
                EmpName = "Rajat Sharma",
                EmailID = "Rajat@kcsitglobal.com",
                Gender = "Male",
                EmpAddress = "Gujarat University",
                Pincode = "382144",
                DateOfBirth = "12/12/2022"
            };

            // Arrange & Act
            _mockEmployeeDetailsService.Setup(x => x.AddAsync(It.IsAny<EmployeeDetails>())).ReturnsAsync(10001);
            var employeeController = new EmployeeController(_mockEmployeeDetailsService.Object);
            var result = await employeeController.InsertEmployeeDetails(employeeDetails);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateEmployeeDetails_ExpectedBehavior()
        {
            //Arrange
            EmployeeDetails employeeDetails = new EmployeeDetails()
            {
                EmpID = 10001,
                EmpName = "Rajat Sharma - India",
                EmailID = "Rajat@kcsitglobal.com",
                Gender = "Male",
                EmpAddress = "Gujarat University",
                Pincode = "382144",
                DateOfBirth = "12/12/2022"
            };

            // Arrange & Act
            _mockEmployeeDetailsService.Setup(x => x.UpdateAsync(It.IsAny<EmployeeDetails>())).ReturnsAsync(true);
            var employeeController = new EmployeeController(_mockEmployeeDetailsService.Object);
            var result = await employeeController.UpdateEmployeeDetails(employeeDetails);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteEmployeeDetails_ExpectedBehavior()
        {
            //Arrange
            EmployeeDetails employeeDetails = new EmployeeDetails()
            {
                EmpID = 10001,
                EmpName = "Rajat Sharma - India",
                EmailID = "Rajat@kcsitglobal.com",
                Gender = "Male",
                EmpAddress = "Gujarat University",
                Pincode = "382144",
                DateOfBirth = "12/12/2022"
            };

            // Arrange & Act
            _mockEmployeeDetailsService.Setup(x => x.DeleteAsync(It.IsAny<EmployeeDetails>())).ReturnsAsync(true);
            var employeeController = new EmployeeController(_mockEmployeeDetailsService.Object);
            var result = await employeeController.DeleteEmployeeDetails(employeeDetails);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateEmployeeEmail_ExpectedBehavior()
        {
            // Arrange & Act
            _mockEmployeeDetailsService.Setup(x => x.UpdateEmployeeEmail(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
            var employeeController = new EmployeeController(_mockEmployeeDetailsService.Object);
            var result = await employeeController.UpdateEmployeeEmail(10001, "Rajat@kcsitglobal.com");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteEmployeeId_True_ExpectedBehavior()
        {
            // Arrange & Act
            _mockEmployeeDetailsService.Setup(x => x.DeleteEmployeebyid(It.IsAny<int>())).ReturnsAsync(true);
            var employeeController = new EmployeeController(_mockEmployeeDetailsService.Object);
            var result = await employeeController.DeleteEmployeebyid(10001);

            // Assert
            Assert.NotNull(result);
        }
    }
}
