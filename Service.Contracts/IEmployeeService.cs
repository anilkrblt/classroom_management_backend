using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        // Get all employees
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(bool trackChanges);

        // Get a specific employee by ID
        Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId, bool trackChanges);

        // Create a new employee
        Task CreateEmployeeAsync(EmployeeDto employeeDto);

        // Update an existing employee
        Task UpdateEmployeeAsync(int employeeId, EmployeeDto employeeDto);

        // Delete an employee
        Task DeleteEmployeeAsync(int employeeId);
    }
}
