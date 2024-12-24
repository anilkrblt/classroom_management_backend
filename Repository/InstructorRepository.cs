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
                .OrderBy(i => i.Name) 
                .ToListAsync();
        }

        public async Task<Instructor> GetInstructorAsync(int instructorId, bool trackChanges)
        {
            return await FindByCondition(i => i.InstructorId == instructorId, trackChanges).SingleOrDefaultAsync();
        }

        public void CreateInstructor(Instructor instructor)
        {
            Create(instructor);
        }

        public async Task UpdateInstructorAsync(Instructor instructor)
        {
            var existingInstructor = await GetInstructorAsync(instructor.InstructorId, true);
            if (existingInstructor != null)
            {
                existingInstructor.Name = instructor.Name;
                existingInstructor.Email = instructor.Email;
                existingInstructor.Password = instructor.Password;
                existingInstructor.Title = instructor.Title;
                existingInstructor.IsAdmin = instructor.IsAdmin;
                existingInstructor.DepartmentId = instructor.DepartmentId;

                Update(existingInstructor);
            }
        }

        public async Task DeleteInstructorAsync(Instructor instructor)
        {
            var existingInstructor = await GetInstructorAsync(instructor.InstructorId, true);
            if (existingInstructor != null)
            {
                Delete(existingInstructor);
            }
        }
    }
}
