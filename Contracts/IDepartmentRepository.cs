using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges);
        Task<Department> GetDepartmentAsync(int departmentId, bool trackChanges);
        Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        


    }
}