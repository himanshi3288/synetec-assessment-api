using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class BonusCalculator : IBonusCalculator
    {
        private readonly IEmployeeService _employeeService;
        public BonusCalculator(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        public async Task<BonusPoolCalculatorResultDto> Calculate(int employeeId, int bonusPoolAmount)
        {
            //load the details of the selected employee using the Id
            var employee = await _employeeService.GetAsync(employeeId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException($"Employee not found for id - {employeeId }");
            }

            //get the total salary budget for the company
            var sumOfSalaries = _employeeService.GetQueryable().Sum(emp => emp.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)sumOfSalaries;
            
            var bonusAllocation = bonusPercentage * bonusPoolAmount;

            return new BonusPoolCalculatorResultDto
            {
                Employee = new EmployeeDto
                {
                    Fullname = employee.Fullname,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    Department = new DepartmentDto
                    {
                        Title = employee.Department.Title,
                        Description = employee.Department.Description
                    }
                },

                Amount = Math.Round(bonusAllocation, 2) 
            };
        }
    }
}
