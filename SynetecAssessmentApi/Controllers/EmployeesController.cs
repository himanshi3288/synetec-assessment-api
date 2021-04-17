using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IBonusCalculator _bonusCalculator;
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IBonusCalculator bonusCalculator, IEmployeeService employeeService)
        {
            _bonusCalculator = bonusCalculator ?? throw new ArgumentNullException(nameof(bonusCalculator));
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }
        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            return await _employeeService.GetAsync();
        }

        [HttpGet]
        [Route("{employeeId}/bonus")]
        public async Task<BonusPoolCalculatorResultDto> CalculateBonus([FromRoute][Required] int employeeId, [FromQuery][Required] int totalBonusPool)
        {
            return await _bonusCalculator.Calculate(employeeId, totalBonusPool);
        }
    }
}
