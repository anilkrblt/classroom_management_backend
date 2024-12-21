using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(d => d.Name) 
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentAsync(int departmentId, bool trackChanges)
        {
            return await FindByCondition(d => d.DepartmentId == departmentId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(d => ids.Contains(d.DepartmentId), trackChanges)
                .ToListAsync();
        }
    }
}
