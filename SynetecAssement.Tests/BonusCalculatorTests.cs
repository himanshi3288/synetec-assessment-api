using System;
using Xunit;
using Moq;
using FluentAssertions;
using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Exceptions;
using System.Threading.Tasks;
using SynetecAssessmentApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SynetecAssement.Tests
{
    public class BonusCalculatorTests
    {
        [Fact]
        public void When_EmployeeNotFound_Then_Exception()
        {
            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((EmployeeDto)null);

            var testEmployeeId = 5;

            var bonusCalculator = new BonusCalculator(mockEmployeeService.Object);

            Func<Task> action = async () => { await bonusCalculator.Calculate(testEmployeeId, 1000); };

            action.Should().Throw<EmployeeNotFoundException>().WithMessage($"Employee not found for id - {testEmployeeId }");
        }

        [Theory]
        [InlineData(1, 10000, 916.38)]
        [InlineData(2, 10000, 1374.57)]
        [InlineData(1, 15000, 1374.57)]
        [InlineData(3, 16000, 2321.50)]
        public async Task When_EmployeeFound_Then_CalculateBonus(int employeeId, int totalBonusPool, decimal expectedBonus)
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionBuilder.UseInMemoryDatabase(databaseName: "HrDb");

            var context = new AppDbContext(dbContextOptionBuilder.Options);

            //test can break if seed data changes. 
            // we should have our own seed/mock data 
            if (!context.Employees.Any())
            {
                DbContextGenerator.SeedData(context);
            }

            var employeeService = new EmployeeService(context);

            var bonusCalculator = new BonusCalculator(employeeService);

            var bonus = await bonusCalculator.Calculate(employeeId, totalBonusPool);

            bonus.Should().NotBeNull().And.BeOfType<BonusPoolCalculatorResultDto>();
            bonus.Amount.Should().Be(expectedBonus);

        }
    }
}
