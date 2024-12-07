using DapperCRUDAngular.Abstraction.Models;
using DapperCRUDAngular.Abstraction.Repository;
using Moq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace DapperCRUDAngular.Services.Tests
{
    public class BaseServiceTest
    {
        private readonly Mock<IBaseRepository<BaseEntity>> _mockBaseRepository;
        public BaseServiceTest()
        {
            _mockBaseRepository = new Mock<IBaseRepository<BaseEntity>>();
        }

        [Fact]
        public async Task AddSync_True_ExpectedBehavior()
        {
            //Arrange
            var employeeDetails = new EmployeeDetails()
            {
                EmpName = "Rajat Sharma",
                EmailID = "Rajat@kcsitglobal.com",
                Gender = "Male",
                EmpAddress = "Gujarat University",
                Pincode = "382144",
                DateOfBirth = "12/12/2022"
            };

            // Arrange & Act
            _mockBaseRepository.Setup(x => x.AddAsync(It.IsAny<EmployeeDetails>())).ReturnsAsync(1);
            var baseService = new BaseService<BaseEntity>(_mockBaseRepository.Object);
            var result = await baseService.AddAsync(employeeDetails);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteSync_True_ExpectedBehavior()
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
            _mockBaseRepository.Setup(x => x.DeleteAsync(It.IsAny<EmployeeDetails>())).ReturnsAsync(true);
            var baseService = new BaseService<BaseEntity>(_mockBaseRepository.Object);
            var result = await baseService.DeleteAsync(employeeDetails);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSync_False_ExpectedBehavior()
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
            _mockBaseRepository.Setup(x => x.DeleteAsync(It.IsAny<EmployeeDetails>())).ReturnsAsync(false);
            var baseService = new BaseService<BaseEntity>(_mockBaseRepository.Object);
            var result = await baseService.DeleteAsync(employeeDetails);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetSync_True_ExpectedBehavior()
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
            _mockBaseRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(employeeDetails);
            var baseService = new BaseService<BaseEntity>(_mockBaseRepository.Object);
            var result = await baseService.GetAsync(1001);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetListSync_True_ExpectedBehavior()
        {
            //Arrange
            List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
            employeeDetails.Add(new EmployeeDetails
            {
                EmpID = 10001,
                EmpName = "Rajat Sharma",
                EmailID = "Test@kcsitglobal.com",
                Gender = "Male",
                EmpAddress = "Gujarat University",
                Pincode = "382144",
                DateOfBirth = "12/12/2022"
            });

            // Arrange & Act
            _mockBaseRepository.Setup(x => x.GetListAsync()).ReturnsAsync(employeeDetails);
            var baseService = new BaseService<BaseEntity>(_mockBaseRepository.Object);
            var result = await baseService.GetListAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateSync_True_ExpectedBehavior()
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
            _mockBaseRepository.Setup(x => x.UpdateAsync(It.IsAny<EmployeeDetails>())).ReturnsAsync(true);
            var baseService = new BaseService<BaseEntity>(_mockBaseRepository.Object);
            var result = await baseService.UpdateAsync(employeeDetails);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAll_True_ExpectedBehavior()
        {
            //Arrange
            Pagination pagination = new Pagination()
            {
                PageIndex = 1,
                PageSize = 10,
                TotalPages = 10,
                TotalRecords = 10
            };
            List<BaseEntity> responseList = new List<BaseEntity>();
            BaseEntity baseEntity = new BaseEntity()
            {
                RowNumber = 1,
                TotalRecords = 10
            };
            responseList.Add(baseEntity);

            // Arrange & Act
            _mockBaseRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<BaseEntity, bool>>>(), It.IsAny<string>(), It.IsAny<Pagination>())).ReturnsAsync(responseList);
            var baseService = new BaseService<BaseEntity>(_mockBaseRepository.Object);
            var result = await baseService.GetAll("EmpID", pagination);

            // Assert
            Assert.NotNull(result);
        }
    }
}
