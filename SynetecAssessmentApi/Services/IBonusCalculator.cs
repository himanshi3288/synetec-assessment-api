using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    /// <summary>
    /// Service responsible for calculation of Bonus.
    /// </summary>
    public interface IBonusCalculator
    {
    
        /// <summary>
        /// Method responsible for calculating bonus for an employee.
        /// </summary>
        /// <param name="employeeId">Id of the Employee</param>
        /// <returns>
        /// Bonus Amount.
        /// Throws EmployeeNotFound exception in case employee is not found in the system.
        /// </returns>
        Decimal Calculate(int employeeId);
    }
}
