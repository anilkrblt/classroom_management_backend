using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class InstructorRepository : RepositoryBase<Instructor>, IInstructorRepository
    {
        public InstructorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(i => i.Name) // Eğitmenleri soyadına göre sıralar
                .ToListAsync();
        }

        public async Task<Instructor> GetInstructorAsync(int instructorId, bool trackChanges)
        {
            return await FindByCondition(i => i.InstructorId == instructorId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Instructor>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(i => ids.Contains(i.InstructorId), trackChanges).ToListAsync();
        }

        public void DeleteInstructor(Instructor instructor)
        {
            Delete(instructor);
        }

        public void CreateInstructorForDepartment(Instructor instructor, int departmentId)
        {
            instructor.DepartmentId = departmentId; 
            Create(instructor);
        }
    }
}
