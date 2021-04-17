using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{

    /// <summary>
    /// Service responsible for providing contracts for Employees.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Get all available employees.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDto>> GetAsync();

        /// <summary>
        /// Gets employee on the basis of Id.
        /// </summary>
        /// <param name="id">Id for the employee</param>
        /// <returns>Employee object Or NULL.</returns>
        Task<EmployeeDto> GetAsync(int id);

        /// <summary>
        /// Get Queryable reference to Employee list. 
        /// This enables to perform operations like SUM, COUNT directly on dbContext. But also does not exposes dbContext out of service.
        /// </summary>
        /// <returns></returns>
        IQueryable<Employee> GetQueryable();
    }
}
