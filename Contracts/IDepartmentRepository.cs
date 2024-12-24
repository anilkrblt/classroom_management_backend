using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges);
        Task<Department> GetDepartmentAsync(int departmentId, bool trackChanges);

        void CreateDepartment(Department department);

        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Department department);
    }
}
