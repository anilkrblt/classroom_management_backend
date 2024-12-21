using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ClassRepository : RepositoryBase<Class>, IClassRepository
    {
        public ClassRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Class>> GetAllClasssAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.Room.Name) 
                .ToListAsync();
        }

        public async Task<Class> GetClassAsync(int classId, bool trackChanges)
        {
            return await FindByCondition(c => c.RoomId == classId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Class>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(c => ids.Contains(c.RoomId), trackChanges)
                .ToListAsync();
        }

        public void DeleteClass(Class classEntity)
        {
            Delete(classEntity);
        }

        public void CreateClassForDeparment(Class classEntity, int departmentId)
        {
            classEntity.DepartmentId = departmentId; 
            Create(classEntity);
        }
    }
}
