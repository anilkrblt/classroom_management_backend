using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId, bool trackChanges)
        {
            return await FindByCondition(e => e.EmployeeId == employeeId, trackChanges).SingleOrDefaultAsync();
        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var existingEmployee = await GetEmployeeAsync(employee.EmployeeId, true);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Email = employee.Email;
                existingEmployee.IsAdmin = employee.IsAdmin;

                Update(existingEmployee);
            }
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            var existingEmployee = await GetEmployeeAsync(employee.EmployeeId, true);
            if (existingEmployee != null)
            {
                Delete(existingEmployee);
            }
        }

        public Employee GetEmployeeByEmail(string email)
        {
            var employee = FindByCondition(e => e.Email == email, false).SingleOrDefault();
            if (employee is null)
                return null;
            return employee;
        }
    }
}
