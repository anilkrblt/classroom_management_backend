using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(bool trackChanges);
        Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId, bool trackChanges);
        Task <IEnumerable<EmployeeDto>> GetEmployeesByIdsAsync(IEnumerable<int> ids, bool trackChanges);

        Task DeleteEmployeeAsync(int employeeId, bool trackChanges);

        Task UpdateEmployeeAsync(int employeeId, EmployeeDto employeeForUpdate, bool trackChanges);

        Task<EmployeeDto> CreateEmployee(EmployeeDto employeeForCreation, bool trackChanges);
    }
}