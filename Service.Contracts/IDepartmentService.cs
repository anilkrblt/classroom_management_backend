using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(bool trackChanges);
        Task<DepartmentDto> GetDepartmentByIdAsync(int departmentId, bool trackChanges);
        Task<IEnumerable<DepartmentDto>> GetDepartmentsByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    }
}