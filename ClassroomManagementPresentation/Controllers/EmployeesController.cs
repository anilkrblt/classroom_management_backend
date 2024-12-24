using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace class_management_backend.controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _serviceManager.EmployeeService.GetAllEmployeesAsync(trackChanges: false);
            return Ok(employees);
        }

        // GET: api/Employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _serviceManager.EmployeeService.GetEmployeeByIdAsync(id, trackChanges: false);

            if (employee == null)
                return NotFound($"Employee with ID {id} not found.");

            return Ok(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("EmployeeDto object is null.");

            await _serviceManager.EmployeeService.CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = employeeDto.EmployeeId }, employeeDto);
        }

        // PUT: api/Employees/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("EmployeeDto object is null.");

            await _serviceManager.EmployeeService.UpdateEmployeeAsync(id, employeeDto);
            return NoContent();
        }

        // DELETE: api/Employees/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await _serviceManager.EmployeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
