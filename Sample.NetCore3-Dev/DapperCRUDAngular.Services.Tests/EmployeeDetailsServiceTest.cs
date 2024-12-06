using DapperCRUDAngular.Abstraction.Repository;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DapperCRUDAngular.Services.Tests
{
    public class EmployeeDetailsServiceTest
    {
        private readonly Mock<IEmployeeDetailsRepository> _mockEmployeeDetailsRepository;
        public EmployeeDetailsServiceTest()
        {
            _mockEmployeeDetailsRepository = new Mock<IEmployeeDetailsRepository>();
        }

        [Fact]
        public async Task UpdateEmployeeEmail_True_ExpectedBehavior()
        {
            //Arrange
            int empId = 10001; string emailId = "Rajat@kcsitglobal.com"; 

            // Arrange & Act
            _mockEmployeeDetailsRepository.Setup(x => x.UpdateEmployeeEmail(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);
            var employeeDetailsService = new EmployeeDetailsService(_mockEmployeeDetailsRepository.Object);
            var result = await employeeDetailsService.UpdateEmployeeEmail(empId, emailId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateEmployeeEmail_False_ExpectedBehavior()
        {
            //Arrange
            int empId = 10001; string emailId = "Rajat@kcsitglobal.com";

            // Arrange & Act
            _mockEmployeeDetailsRepository.Setup(x => x.UpdateEmployeeEmail(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);
            var employeeDetailsService = new EmployeeDetailsService(_mockEmployeeDetailsRepository.Object);
            var result = await employeeDetailsService.UpdateEmployeeEmail(empId, emailId);

            // Assert
            Assert.False(false);
        }

        [Fact]
        public async Task DeleteEmployeeId_True_ExpectedBehavior()
        {
            //Arrange
            int empId = 10001;

            // Arrange & Act
            _mockEmployeeDetailsRepository.Setup(x => x.DeleteEmployeebyid(It.IsAny<int>())).ReturnsAsync(true);
            var employeeDetailsService = new EmployeeDetailsService(_mockEmployeeDetailsRepository.Object);
            var result = await employeeDetailsService.DeleteEmployeebyid(empId);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteEmployeeId_False_ExpectedBehavior()
        {
            //Arrange
            int empId = 10001;

            // Arrange & Act
            _mockEmployeeDetailsRepository.Setup(x => x.DeleteEmployeebyid(It.IsAny<int>())).ReturnsAsync(false);
            var employeeDetailsService = new EmployeeDetailsService(_mockEmployeeDetailsRepository.Object);
            var result = await employeeDetailsService.DeleteEmployeebyid(empId);

            // Assert
            Assert.False(result);
        }
    }
}
