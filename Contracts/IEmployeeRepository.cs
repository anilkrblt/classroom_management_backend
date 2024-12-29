using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges);
        Task<Employee> GetEmployeeAsync(int employeeId, bool trackChanges);

        Employee AuthenticateEmployee(string email, string password);


        void CreateEmployee(Employee employee);

        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
    }
}
