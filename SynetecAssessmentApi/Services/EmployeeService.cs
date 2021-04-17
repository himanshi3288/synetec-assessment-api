using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _dbContext;
        public EmployeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<EmployeeDto> GetAsync(int id)
        {
            //load the details of the selected employee using the Id
            var employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == id);

            return new EmployeeDto
            {
                Fullname = employee.Fullname,
                JobTitle = employee.JobTitle,
                Salary = employee.Salary,
                Department = new DepartmentDto
                {
                    Title = employee.Department.Title,
                    Description = employee.Department.Description
                }
            };
        }

        public async Task<IEnumerable<EmployeeDto>> GetAsync()
        {
            IEnumerable<Employee> employees = await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();

            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return result;
        }

        public IQueryable<Employee> GetQueryable()
        {
            return _dbContext.Employees;
        }
    }
}
