using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace class_management_backend.controllers
{
    [Route("api/employees")]
    [ApiController]


    public class EmployeesController : ControllerBase
    {

        private readonly IServiceManager _servisManager;
        public EmployeesController(IServiceManager serviceManager)
        {
            _servisManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees =await _servisManager.EmployeeService.GetAllEmployeesAsync(false);
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees([FromRoute(Name ="id")] int employeeId )
        {
            var employee =await _servisManager.EmployeeService.GetEmployeeByIdAsync(employeeId,false);
            return Ok(employee);
        }

    }
}