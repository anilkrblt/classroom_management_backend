using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {

        Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges);
        Task<Employee> GetEmployeeAsync(int employeeId, bool trackChanges);
        Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteEmployee(Employee Employee);
        void CreateEmployee(Employee Employee);


    }
}