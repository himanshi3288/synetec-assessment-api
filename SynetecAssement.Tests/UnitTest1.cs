using System;
using Xunit;

namespace SynetecAssement.Tests
{
    public class BonusCalculatorTests
    {
        [Fact]
        public void When_EmployeeNotFound_Then_Exception()
        {
            throw new NotImplementedException();
        }

        [Theory]
        [InlineData(1, 10000, 500)]
        [InlineData(2, 10000, 500)]
        [InlineData(1, 15000, 500)]
        [InlineData(3, 16000, 500)]
        public void When_EmployeeFound_Then_CalculateBonus(int employeeId, int totalBonusPool, decimal expectedBonus)
        {
            throw new NotImplementedException();
        }
    }
}
