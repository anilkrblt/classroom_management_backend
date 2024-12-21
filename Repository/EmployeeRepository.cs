using System;
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
            return await FindByCondition(e => e.EmployeeId == employeeId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(e => ids.Contains(e.EmployeeId), trackChanges)
                .ToListAsync();
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }
    }
}
